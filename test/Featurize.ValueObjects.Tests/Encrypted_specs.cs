using Featurize.ValueObjects.Identifiers;
using Featurize.ValueObjects.Identifiers.Behaviours;
using FluentAssertions;
using System.Security.Cryptography;
using System.Text.Json;

namespace Featurize.ValueObjects.Tests;

public class Encrypted_Specs
{
    public class Serialize
    {
        [Test]
        public void can_be_unknown()
        {
            var e = Encrypted<Guid>.Parse("?");

            var cryptor = Aes.Create();
            cryptor.Padding = PaddingMode.PKCS7;

            var value = () => e.Decrypt(cryptor.CreateDecryptor());

            value.Should().NotThrow();
        }

        [Test]
        public void can_be_created()
        {
            var secret = Guid.NewGuid().ToString();
            var cryptor = Aes.Create();
            cryptor.Padding = PaddingMode.PKCS7;

            var enc = Encrypted<string>.Create(secret, cryptor.CreateEncryptor());

            enc.Decrypt(cryptor.CreateDecryptor()).Should().Be(secret);
        }

        [Test]
        public void serialized_as_expected()
        {
            var secret = Guid.NewGuid().ToString();
            var cryptor = Aes.Create();
            cryptor.Padding = PaddingMode.PKCS7;

            var enc = Encrypted<string>.Create(secret, cryptor.CreateEncryptor());

            var json = JsonSerializer.Serialize(enc);

            var result = JsonSerializer.Deserialize<Encrypted<string>>(json);

            result.Should().NotBeNull();
            result!.Decrypt(cryptor.CreateDecryptor()).Should().Be(secret);
        }

        [Test]
        public void serialize_initials_as_expected()
        {
            var secret = Initials.Parse("P.M.D.");
            var cryptor = Aes.Create();
            cryptor.Padding = PaddingMode.PKCS7;

            var enc = Encrypted<Initials>.Create(secret, cryptor.CreateEncryptor());

            var json = JsonSerializer.Serialize(enc);

            var result = JsonSerializer.Deserialize<Encrypted<Initials>>(json);

            result.Should().NotBeNull();
            result!.Decrypt(cryptor.CreateDecryptor()).Should().Be(secret);
        }

        [Test]
        public void serialize_id_as_expected()
        {
            var secret = Id<GuidBehaviour>.Next();
            var cryptor = Aes.Create();
            cryptor.Padding = PaddingMode.PKCS7;

            var enc = Encrypted<Id<GuidBehaviour>>.Create(secret, cryptor.CreateEncryptor());

            var json = JsonSerializer.Serialize(enc);

            var result = JsonSerializer.Deserialize<Encrypted<Id<GuidBehaviour>>>(json);

            result.Should().NotBeNull();
            result!.Decrypt(cryptor.CreateDecryptor()).Should().Be(secret);
        }
    }
}