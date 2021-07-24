# OAML

This is the official library for the Open Anonymized Message Library. 

This library was designed to enable **Validated Anonymized Messaging** with **Source Validation** between two **User Nodes** using **P2P Connections**.

# How It Works

## Step 1: Exchange Keys
You and another party will exchange your public keys generated from your **EncryptionManager** and **SignatureManager**, ideally done in person via USB securely. Keys can be generated using the **KeyGeneratorForm**.
[Insert Diagram]

## Step 2: Configure Application
Upon application start, a server listener is initialized for oncoming connections. You can set your listening port as well as your data/signature keys in the **OptionsForm**. 
[Insert Diagram]

You can then right click the User Node List and select **Add User**, this will open a form where you must configure the **remote endpoint** and **port**. This information is used to map the incoming connection to your specified **UserNode**. Also specify the public keys given to you from **Step 1**, these keys will be used to decrypt and validate the message envelope.
[Insert Diagram]

## Step 3: Message Envelope
After selecting a UserNode, you can choose to send a message. The message will be formatted as a **MessageEnvelope** which is composed of the following structure:

    struct Message
    {
    	char[4] _magic;
    	float _version;
    	int _MessageType;
    	bool _encrypted;
    	int _dataLength;
    	byte[] _data;
    	int _signatureLength;
    	byte[] _signature;
    }
It specifies the version of the envelope, the type of message (text, file, img), whether it is encrypted and the data and signature of the data.
[Insert Diagram]

## Step 4: Transit
After the **MessageEnvelope** goes through the **EncryptionManager** and **SignatureManager**, the original message will be encrypted using the UserNode's **Public Data Key** and signed with your **Private Signature Key**.

*(This ensures that **only the node on the other end** has the ability to **decrypt the message** whilst also being able to **verify the contents** of the decrypted message were **sent by you**.)*

A new **ChatClient** is intialized with the configured remote endpoint and port, opening a **temporary TCP connection** which sends the **MessageEnvelope** to the listening server.  
*(Note: remote UserNode must be active to receive message)*

## Step 5: Processing
After receiving the **MessageEnvelope** the **ChatServer** on your machine maps the remote endpoint and port to a **UserNode** within your configuration. There it will use the specified **Encryption and Signature Keys** to decrypt and validate the message.

The message by default is kept only in memory and disposed of when your application exits; however, this is configurable by specifying the callback handler for your own ideal implementation.

# Encryption

OAML utilizes an EncryptionManager interface which allows you to specify your own implementations of encryption. By default, RSA 2048 bit is used.

    public interface IEncryptionManager
    {
        EncryptionMethod _CryptMethod { get; }
    
        int _KeySize { get; }
    
        KeyPair _Keys { get; }
    
        byte[] Encrypt(string key, byte[] data);
    
        byte[] Decrypt(string key, byte[] data);
    
        void LoadKeys(KeyPair Keys);
    
        KeyPair GenerateKeys();
    }

You can see the provided Managers in: `OAML.Security.Encryption`

# Signatures

OAML utilizes a SignatureManager interface which allows you to specify your own implementations of signatures and validation. By default, ECDSA is used.

    public interface ISignatureManager
    {
        byte[] Sign(byte[] key, byte[] data);

        bool Verify(byte[] key, byte[] data, byte[] signature);

        void LoadKeys(KeyPair Keys);

        KeyPair GenerateKeys();
    }

You can see the provided Managers in: `OAML.Security.Signatures`

# TODO:

#### Error and Usage Handling for:

 - Closed connections during message transactions
 - Encryption Fails (bad data/key)
 - Version of Message is higher than Server Application Version

#### Documentation of methods, classes and interfaces

#### Code cleanup and refactoring
