namespace MongoDb.Controllers
{
    public class Funcoes
    {
        public Funcoes()
        {

        }
        public static bool Par(int num)
        {
            if (num % 2 == 0)
                return true;
            else
                return false;
        }

        public  int PaginaAtual(int num)
        {
            if (num == 1) return 1;
            if (Par(num))
                return num / 2;
            else
                return (num + 1) / 2;
        }
    }
}
