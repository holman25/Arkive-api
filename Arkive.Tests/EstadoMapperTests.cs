using Xunit;

namespace Arkive.Tests;

public class EstadoMapperTests
{
    [Theory]
    [InlineData("Registrado", 0)]
    [InlineData("Pendiente", 1)]
    [InlineData("Validado", 2)]
    [InlineData("Archivado", 3)]
    [InlineData("otro", -1)]
    [InlineData("", -1)]
    public void MapEstadoToCode_Works(string input, int expected)
    {
        int actual = MapEstadoToCode(input);
        Assert.Equal(expected, actual);
    }

    private static int MapEstadoToCode(string? estado)
    {
        if (string.IsNullOrWhiteSpace(estado)) return -1;
        if (estado.Equals("Registrado", StringComparison.OrdinalIgnoreCase)) return 0;
        if (estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase)) return 1;
        if (estado.Equals("Validado", StringComparison.OrdinalIgnoreCase)) return 2;
        if (estado.Equals("Archivado", StringComparison.OrdinalIgnoreCase)) return 3;
        return -1;
    }
}
