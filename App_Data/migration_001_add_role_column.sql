-- ============================================================
-- Migration 001: Add 'role' column to Register table
-- Run this BEFORE deploying the updated application code.
-- ============================================================

-- 1. Add the role column (default 'user' for all existing rows)
ALTER TABLE Register
ADD role VARCHAR(20) NOT NULL DEFAULT 'user';

-- 2. Promote existing admin account(s)
--    Adjust the WHERE clause to match your actual admin email(s).
UPDATE Register
SET role = 'admin'
WHERE email LIKE 'admin%';

-- 3. (Optional) Re-hash existing plaintext passwords
--    This must be done via application code or a one-time script
--    because PBKDF2 hashing is not available in T-SQL natively.
--    The updated PasswordHelper.VerifyPassword() includes a
--    legacy-plaintext fallback so the app will still work
--    with old passwords until they are migrated.
