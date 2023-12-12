namespace FeatureFlag.UnitTests.Setup;

[ExcludeFromCodeCoverage]
public abstract class UnitTestBase
{
    public DateTime DefaultAuditDate => new(2011, 11, 11, 11, 11, 11);

    public DateTime DefaultStartDate => new(2021, 11, 11);

    public DateTime DefaultEndDate => new(2023, 11, 11);

    public DateTime StaticTestDate01 => new(2023, 11, 11, 12, 12, 12);

    public TimeOnly StaticTestTime01 => new(21, 15, 18);
}
