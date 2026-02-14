namespace Industria;

public class Industria
{
    public int QuantidadeEmpregados { get; set; } = 625;
    public double SalarioEmpregado { get; set; } = 10000;
    public double Caixa { get; set; } = 500000000.00;

    public double custoProducao { get; set; } = 42.75;
    public double precoVenda { get; set; } = 75.00;

    // =============================
    // FOLHA SALARIAL
    // =============================

    public double FolhaSalarial()
    {
        return QuantidadeEmpregados * SalarioEmpregado;
    }

    // 61% que a empresa paga sobre a folha
    public double ArrecadacaoImpostoIndustria()
    {
        return FolhaSalarial() * 0.61;
    }

    // 25% que o funcionário paga
    public double ImpostoSalario()
    {
        return SalarioEmpregado * 0.25;
    }

    public double ArrecadacaoImpostoSalarioIndustria()
    {
        return QuantidadeEmpregados * ImpostoSalario();
    }

    // =============================
    // IMPOSTO SOBRE VENDAS (18%)
    // =============================

    public double ImpostoVendaUnitario()
    {
        return precoVenda * 0.18;
    }

    public double ReceitaLiquidaUnitario()
    {
        return precoVenda - ImpostoVendaUnitario();
    }

    // Custo total de produção
    public double CustoProducaoTotal(int quantidade)
    {
        return quantidade * custoProducao;
    }
}
