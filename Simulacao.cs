using Comercio;
using Industria;
using Prefeitura;

namespace Simulacao;

public class Simulacao
{
    public static void Main(string[] args)
    {
        Comercio.Comercio comercio = new Comercio.Comercio();
        Industria.Industria industria = new Industria.Industria();
        Prefeitura.Prefeitura prefeitura = new Prefeitura.Prefeitura();

        int mes = 0;
        int ano = 0;
        int cicloGestao = 0;

        bool sistemaAtivo = true;

        while (sistemaAtivo)
        {
            mes++;

            Console.WriteLine($"\n================ MÊS {mes} ================\n");

            // =============================
            // 1 - PAGAMENTO DE SALÁRIOS
            // =============================

            // COMÉRCIO
            double folhaComercio = comercio.FolhaSalarial();
            double impostoEmpresaComercio = comercio.ArrecadacaoImpostoComercio();
            double impostoFuncionarioComercio = comercio.ArrecadacaoImpostoSalarioComercio();

            double custoTotalComercio = folhaComercio + impostoEmpresaComercio;

            if (comercio.Caixa < custoTotalComercio)
            {
                Console.WriteLine("Comércio não conseguiu pagar os salários.");
                break;
            }

            comercio.Caixa -= custoTotalComercio;
            prefeitura.Caixa += impostoEmpresaComercio + impostoFuncionarioComercio;

            // INDÚSTRIA
            double folhaIndustria = industria.FolhaSalarial();
            double impostoEmpresaIndustria = industria.ArrecadacaoImpostoIndustria();
            double impostoFuncionarioIndustria = industria.ArrecadacaoImpostoSalarioIndustria();

            double custoTotalIndustria = folhaIndustria + impostoEmpresaIndustria;

            if (industria.Caixa < custoTotalIndustria)
            {
                Console.WriteLine("Indústria não conseguiu pagar os salários.");
                break;
            }

            industria.Caixa -= custoTotalIndustria;
            prefeitura.Caixa += impostoEmpresaIndustria + impostoFuncionarioIndustria;

            // PREFEITURA (não paga imposto sobre folha)
            double folhaPrefeitura = prefeitura.QuantidadeEmpregados * prefeitura.SalarioEmpregado;
            double custoBolsa = prefeitura.QuantidadeBeneficiarios * prefeitura.ValorBolsa;

            prefeitura.Caixa -= (folhaPrefeitura + custoBolsa);

            // =============================
            // 2 - POPULAÇÃO CONSOME TUDO
            // =============================

            int populacaoAtiva = 1000;
            int beneficiarios = prefeitura.QuantidadeBeneficiarios;

            int totalConsumidores = populacaoAtiva + beneficiarios;

            // Cada pessoa compra 1 item por mês
            int itensVendidos = totalConsumidores;

            double totalVenda = itensVendidos * comercio.precoVenda;
            double impostoVendaComercio = totalVenda * 0.38;

            comercio.Caixa += (totalVenda - impostoVendaComercio);
            prefeitura.Caixa += impostoVendaComercio;

            // =============================
            // 3 - COMÉRCIO REPÕE ESTOQUE
            // =============================

            double custoReposicao = itensVendidos * comercio.custoReposicaoEstoque;
            double impostoVendaIndustria = custoReposicao * 0.18;
            double custoProducao = itensVendidos * industria.custoProducao;

            if (comercio.Caixa < custoReposicao)
            {
                Console.WriteLine("Comércio não conseguiu repor estoque.");
                break;
            }

            if (industria.Caixa < custoProducao)
            {
                Console.WriteLine("Indústria não conseguiu produzir.");
                break;
            }

            comercio.Caixa -= custoReposicao;
            industria.Caixa += (custoReposicao - impostoVendaIndustria);
            prefeitura.Caixa += impostoVendaIndustria;

            industria.Caixa -= custoProducao;

            // =============================
            // RELATÓRIO MENSAL
            // =============================

            Console.WriteLine($"Caixa Prefeitura: R$ {prefeitura.Caixa:N2}");
            Console.WriteLine($"Caixa Comércio: R$ {comercio.Caixa:N2}");
            Console.WriteLine($"Caixa Indústria: R$ {industria.Caixa:N2}");

            Console.WriteLine("\nImpostos do mês:");
            Console.WriteLine($"Comércio pagou: R$ {(impostoEmpresaComercio + impostoFuncionarioComercio + impostoVendaComercio):N2}");
            Console.WriteLine($"Indústria pagou: R$ {(impostoEmpresaIndustria + impostoFuncionarioIndustria + impostoVendaIndustria):N2}");
            Console.WriteLine($"População pagou (IR salários): R$ {(impostoFuncionarioComercio + impostoFuncionarioIndustria):N2}");

            // =============================
            // CONTROLE DE ANO E GESTÃO
            // =============================

            if (mes % 12 == 0)
            {
                ano++;
                Console.WriteLine($"\n==== 1 ANO COMPLETO ({ano}) ====");

                if (ano % 4 == 0)
                {
                    cicloGestao++;
                    Console.WriteLine($"==== NOVO CICLO DE GESTÃO ({cicloGestao}) ====");
                }
            }

            // Condição de parada principal
            if (industria.Caixa <= 0)
            {
                Console.WriteLine("Indústria ficou sem dinheiro.");
                sistemaAtivo = false;
            }
        }

        Console.WriteLine("\n=================================");
        Console.WriteLine($"Total de meses simulados: {mes}");
        Console.WriteLine($"Total de ciclos de gestão: {cicloGestao}");
        Console.WriteLine("Simulação encerrada.");
    }
}
