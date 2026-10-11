# Glossary

**Password hasher**: the module that turns a password and optional salt into a hash (`PasswordHasher.Hash`). Owns salt normalisation (blank means no salt) and the fixed PBKDF2 parameters (HMAC-SHA1, 1000 iterations, 32 bytes), which are an output contract.

**Salt**: optional text mixed into the hash. Null, empty and whitespace all mean "no salt".

**Hash**: uppercase hex string of the derived key.
