namespace Comercio;

public class Comercio
{
    public int QuantidadeEmpregados { get; set; } = 250;
    public double SalarioEmpregado { get; set; } = 7500;
    public double Caixa { get; set; } = 100000000.00;

    public double custoReposicaoEstoque { get; set; } = 75.00;
    public double precoVenda { get; set; } = 203.00;

    // =============================
    // FOLHA SALARIAL
    // =============================

    public double FolhaSalarial()
    {
        return QuantidadeEmpregados * SalarioEmpregado;
    }

    // 61% que a empresa paga sobre a folha
    public double ArrecadacaoImpostoComercio()
    {
        return FolhaSalarial() * 0.61;
    }

    // 25% que o funcionário paga
    public double ImpostoSalario()
    {
        return SalarioEmpregado * 0.25;
    }

    public double ArrecadacaoImpostoSalarioComercio()
    {
        return QuantidadeEmpregados * ImpostoSalario();
    }

    // =============================
    // IMPOSTO SOBRE VENDAS (38%)
    // =============================

    public double ImpostoVendaUnitario()
    {
        return precoVenda * 0.38;
    }

    public double ReceitaLiquidaUnitario()
    {
        return precoVenda - ImpostoVendaUnitario();
    }
}
