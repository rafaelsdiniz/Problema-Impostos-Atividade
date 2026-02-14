namespace Prefeitura;

public class Prefeitura
{
    public double Caixa { get; set; } = 54900000.00;

    public int QuantidadeEmpregados { get; set; } = 125;
    public double SalarioEmpregado { get; set; } = 20000;

    public int QuantidadeBeneficiarios { get; set; } = 1200;
    public double ValorBolsa { get; set; } = 793.33;

    // =============================
    // FOLHA PREFEITURA
    // =============================

    public double FolhaSalarial()
    {
        return QuantidadeEmpregados * SalarioEmpregado;
    }

    public double CustoBeneficios()
    {
        return QuantidadeBeneficiarios * ValorBolsa;
    }

    public double GastoTotalMensal()
    {
        return FolhaSalarial() + CustoBeneficios();
    }
}
