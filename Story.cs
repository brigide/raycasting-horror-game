using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace kMissCluster
{
    public struct Line
    {
        public string StoryLine;
        public float FirstAppear;
    }
    public static class Story
    {
        public static Line currentDraw;
        public static Queue<Line> StoryLines;
        public static Queue<string> PlayingStory;
        public static Queue<string> Papers;
        public static Queue<string> Ending1;
        public static Queue<string> Ending2;


        public static void LoadStory()
        {
            StoryLines = new Queue<Line>();
            PlayingStory = new Queue<string>();
            Papers = new Queue<string>();
            Ending1 = new Queue<string>();
            Ending2 = new Queue<string>();

            LoadStoryLines();
            LoadPlayingLines();
            LoadPapers();
            LoadEnding1();
            LoadEnding2();

            currentDraw = StoryLines.Peek();
        }

        private static void LoadStoryLines()
        {
            StoryLines.Enqueue(new Line { StoryLine = "O ano e 1959.", FirstAppear = 0.0f });
            StoryLines.Enqueue(new Line { StoryLine = "Nessa data, acontecia um misterioso caso de desaparecimento na cidade de\nSaint Bernard, no Brazil.", FirstAppear = 0.0f });
            StoryLines.Enqueue(new Line { StoryLine = "As 8 vitimas, estudantes e formandos da primeira turma de Ciencia da Computacao\ndo conceituado Centro Universitario da FAI, foram vistos pela ultima vez bebendo\nalegremente num bar proximo a Universidade conhecido como Little Castle Bar.", FirstAppear = 0.0f });
            StoryLines.Enqueue(new Line { StoryLine = "Coitados, era a vaspera da defesa de seus trabalhos de conclusao do curso e os\njovens estavam apenas tentando se destrair apos um longo ano de trabalho.", FirstAppear = 0.0f });
            StoryLines.Enqueue(new Line { StoryLine = "Um amigo proximo a uma das vitimas relatou uma tentativa de censura a um de seus\ntrabalhos e mesmo assim, o caso foi dado como encerrado e as vitimas dadas como\nmortas.", FirstAppear = 0.0f });
            StoryLines.Enqueue(new Line { StoryLine = "Porem algo parece errado, muito suspeita uma tentativa de censura seguida pelo\ndesaparecimento dos estudantes. Quem iria censurar apenas um trabalho academico?\nPor que? Onde estao esses jovens? O que fizeram com eles?", FirstAppear = 0.0f });
            StoryLines.Enqueue(new Line { StoryLine = "A VERDADE DEVE SER REVELADA", FirstAppear = 0.0f });
        }

        private static void LoadPlayingLines()
        {
            PlayingStory.Enqueue("Eu sou o Private Investigator Saboia, fui contratado pela família de uma das vítimas para descobrir o que realmente aconteceu aqui.");
            PlayingStory.Enqueue("Decido ir até a universidade onde a suposta tentativa de censura ocorreu. São 3:00AM, está tudo escuro e não há ningúem aqui. Preciso ser discreto.");
            PlayingStory.Enqueue("Eu preciso encontrar esses trabalhos para entender o que está havendo aqui.");
        }

        private static void LoadPapers()
        {
            Papers.Enqueue("1/8. A Inteligência Artificial vem se tornando uma das tecnologias mais importantes desenvolvidas na década.");
            Papers.Enqueue("2/8. A empresa IBBM já fez grandes avanços na área, os engenheiros da FAI tiveram acesso com exclusividade à sua nova criação: IBBM-PS, um Supercomputador que será determinante para a nova era da computação.");
            Papers.Enqueue("3/8. Apesar dos benefícios, existem estudos identificando padrões de comportamento agressivos e 'anti-humanos'. Nada preocupante devido às limitações de hardware descritas por Tintini (1958). Porém, é algo para se atentar a longo prazo.");
            Papers.Enqueue("4/8. Nos últimos meses, IBBM-PS tem mostrado resultados impressionantes a respeito de reconhecimento facil e processamento de linguagem natural.");
            Papers.Enqueue("5/8. Vem aumentado a preocupação com uma possível rebelião dos robos por parte da população, totalmente falaciosa pelos motivos descritos por Tom-Daniel (1959).");
            Papers.Enqueue("6/8. Em novos testes, IBBM-PS mostrou comportamentos estranhos, parece que não respondia aos nossos comandos. Certamente estava ligado e funcional.");
            Papers.Enqueue("7/8. IBBM-PS está completamente fora de si! Devemos suspender essas pesquisas agora! Kleber foi atacado, ninguém vem nos ajudar. Nos prenderam aqui, parece que sabiam o que iria acontecer.");
            Papers.Enqueue("8/8. IBBM-PS é a técnologia mais inovadora e relevante da última década em seu segmento. IBBM tem planos para distribuição em massa como computadores pessoais.");
        }

        private static void LoadEnding1()
        {
            Ending1.Enqueue("Eles estão aqui.");
        }

        private static void LoadEnding2()
        {
            Ending2.Enqueue("Ninguém saberá o real destino desses meninos.");
        }

        public static void DrawStoryLine(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentDraw.FirstAppear == 0.0f)
            {
                currentDraw.FirstAppear = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            spriteBatch.DrawString(Art.Font, currentDraw.StoryLine, new Vector2(100, Game1.ScreenSize.Y / 2.0f), Color.White);

            if (currentDraw.FirstAppear + 5.0f <= gameTime.TotalGameTime.TotalSeconds)
            {
                StoryLines.Dequeue();
                if (StoryLines.Count > 0) currentDraw = StoryLines.Peek();
            }
        }
    }
}