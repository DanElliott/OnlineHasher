# Glossary

**Password hasher**: the module that turns a password and optional salt into a hash result (`PasswordHasher.Hash`). Owns the Password validity rule, salt normalisation (blank means no salt) and the fixed PBKDF2 parameters (HMAC-SHA1, 1000 iterations, 32 bytes), which are an output contract. Returns a `HashResult`, a closed hierarchy of `HashResult.Success` (hash) or `HashResult.Failure` (error message); never throws for invalid input.

**Password**: the text to hash. Valid when non-empty; whitespace is allowed. The Password hasher is the only place that enforces this.

**Salt**: optional text mixed into the hash. Null, empty and whitespace all mean "no salt".

**Hash**: uppercase hex string of the derived key.
