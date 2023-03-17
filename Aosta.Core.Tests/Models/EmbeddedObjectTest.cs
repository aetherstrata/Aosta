using System.Diagnostics;
using Realms;

namespace Aosta.Core.Tests.Models;

public class EmbeddedObjectTest
{
    private readonly ComplexEmbeddedData _complexEmbed = new()
    {
        Description = "Embedded Data"
    };

    private readonly EmbeddedData _simpleEmbed = new()
    {
        Description = "Embedded Data",
        Data = "0123456789"
    };

    private Realm _realm = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _complexEmbed.Data.Add(new EmbeddedData
        {
            Description = "Data 0",
            Data = "0"
        });

        _complexEmbed.Data.Add(new EmbeddedData
        {
            Description = "Data 1",
            Data = "1"
        });
    }

    [SetUp]
    public void SetUp()
    {
        _realm = RealmSetup.CreateNewRealm();
    }

    [Test]
    public void SimpleEmbeddingTest()
    {
        var id = Guid.Empty;

        _realm.Write(() =>
        {
            id = _realm.Add(new EmbedTestObject
            {
                Name = "Test",
                EmbeddedData = _simpleEmbed
            }).Id;
        });

        var testObject = _realm.Find<EmbedTestObject>(id);

        Assert.Multiple(() =>
        {
            Assert.That(testObject.Name, Is.EqualTo("Test"));
            Assert.That(testObject.EmbeddedData, Is.Not.Null);
            Assert.That(testObject.EmbeddedData!.Description, Is.EqualTo("Embedded Data"));
            Assert.That(testObject.EmbeddedData!.Data, Is.EqualTo("0123456789"));
        });
    }

    [Test]
    public void ComplexEmbeddingTest()
    {
        var id = Guid.Empty;

        _realm.Write(() =>
        {
            id = _realm.Add(new EmbedTestObject
            {
                Name = "Test",
                ComplexData = _complexEmbed
            }).Id;
        });

        var testObject = _realm.Find<EmbedTestObject>(id);

        Assert.Multiple(() =>
        {
            Assert.That(testObject.Name, Is.EqualTo("Test"));
            Assert.That(testObject.EmbeddedData, Is.Null);
            Assert.That(testObject.ComplexData, Is.Not.Null);
        });

        Debug.Assert(testObject.ComplexData != null, "testObject.ComplexData != null");

        Assert.Multiple(() =>
        {
            Assert.That(testObject.ComplexData.Description, Is.EqualTo("Embedded Data"));
            Assert.That(testObject.ComplexData.Data, Has.Count.EqualTo(2));
            Assert.That(testObject.ComplexData.Data[0], Is.EqualTo(_complexEmbed.Data[0]));
            Assert.That(testObject.ComplexData.Data[1], Is.EqualTo(_complexEmbed.Data[1]));
        });
    }
}

public partial class EmbedTestObject : IRealmObject
{
    [PrimaryKey] public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public EmbeddedData? EmbeddedData { get; set; }

    public ComplexEmbeddedData? ComplexData { get; set; }
}

public partial class EmbeddedData : IEmbeddedObject
{
    public string Description { get; set; } = string.Empty;

    public string Data { get; set; } = string.Empty;
}

public partial class ComplexEmbeddedData : IEmbeddedObject
{
    public string Description { get; set; } = string.Empty;

    public IList<EmbeddedData> Data { get; } = null!;
}