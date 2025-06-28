# OAML
Open Anonymized Message Library

# TODO
* Prevent DDoS (only accept connections from connections in your json file)
* Add Start/Stop listener for the host
* Look into TLS for negotiating algos.
* Implement digital signatures
* Relay servers (potentially)
* Keep connection open (to receive all blobs)
* Ability to split up blobs for large files
* privatize setup in advance
* figure out a way around state actor interceptions traffic that can work backwards across ISP to goelocate sender
* scrub receiving buffer to prevent overflows
* support compression
* strip ip info from header
* not look at whole packet to prevent resource exhaustion
* block feature (after x incorrect auths)
* 
