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
        public static Line currentPaper;
        public static Line currentLocked;
        public static Line currentEnding2;
        public static Line currentEnding1;
        public static Queue<Line> StoryLines;
        public static Queue<Line> PlayingStory;
        public static Queue<Line> BuildingStory;
        public static Queue<Line> Papers;
        public static Queue<Line> ChoiceLines;
        public static Queue<Line> Ending1;
        public static Queue<Line> Ending2;


        public static void LoadStory()
        {
            StoryLines = new Queue<Line>();
            PlayingStory = new Queue<Line>();
            BuildingStory = new Queue<Line>();
            ChoiceLines = new Queue<Line>();
            Papers = new Queue<Line>();
            Ending1 = new Queue<Line>();
            Ending2 = new Queue<Line>();

            LoadStoryLines();
            LoadPlayingLines();
            LoadBuildingLines();
            LoadPapers();
            LoadChoiceLines();
            LoadEnding1();
            LoadEnding2();

            currentDraw = StoryLines.Peek();
            currentPaper = Papers.Peek();
            currentLocked = new Line { StoryLine = "", FirstAppear = 0.0f };
            currentEnding2 = Ending2.Peek();
            currentEnding1 = Ending1.Peek();
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
            PlayingStory.Enqueue(new Line { StoryLine = "", FirstAppear = 0.0f });
            PlayingStory.Enqueue(new Line { StoryLine = "Eu sou o Private Investigator Saboia, fui contratado pela familia de uma\ndas vitimas para descobrir o que realmente aconteceu aqui.", FirstAppear = 0.0f });
            PlayingStory.Enqueue(new Line { StoryLine = "Decido ir ate a universidade onde a suposta tentativa de censura ocorreu.\nSao 3:00AM, esta tudo escuro e nao ha ninguem aqui. Preciso ser discreto.", FirstAppear = 0.0f });
            PlayingStory.Enqueue(new Line { StoryLine = "Eu preciso encontrar esses trabalhos para entender o que esta havendo aqui.", FirstAppear = 0.0f });
        }

        private static void LoadBuildingLines()
        {
            BuildingStory.Enqueue(new Line { StoryLine = "", FirstAppear = 0.0f });
            BuildingStory.Enqueue(new Line { StoryLine = "Escuto a porta se trancando atras de mim, acho que estou preso.", FirstAppear = 0.0f });
        }
        private static void LoadPapers()
        {
            Papers.Enqueue(new Line { StoryLine = "1/8. A Inteligencia Artificial vem se tornando uma das tecnologias mais importantes\ndesenvolvidas na decada.", FirstAppear = 0.0f });
            Papers.Enqueue(new Line { StoryLine = "2/8. A empresa IBBM ja fez grandes avancos na area, os engenheiros da FAI tiveram\nacesso com exclusividade a sua nova criacao IBBM PS, um Supercomputador que sera\ndeterminante para a nova era da computacao.", FirstAppear = 0.0f });
            Papers.Enqueue(new Line { StoryLine = "3/8. Apesar dos beneficios, existem estudos identificando padroes de comportamento\nagressivos e 'anti-humanos'. Nada preocupante devido as limitacoes de hardware\ndescritas por Tintini (1958). Porem, e algo para se atentar a longo prazo.", FirstAppear = 0.0f });
            Papers.Enqueue(new Line { StoryLine = "4/8. Nos ultimos meses, IBBM-PS tem mostrado resultados impressionantes a respeito\nde reconhecimento facil e processamento de linguagem natural.", FirstAppear = 0.0f });
            Papers.Enqueue(new Line { StoryLine = "5/8. Vem aumentado a preocupacao com uma possivel rebeliao dos robos por parte da\ncomunidade cientifica, totalmente falaciosa pelos motivos descritos por Tom-Daniel (1959).", FirstAppear = 0.0f });
            Papers.Enqueue(new Line { StoryLine = "6/8. Em novos testes, IBBM-PS mostrou comportamentos estranhos, parece que nao\nrespondia aos nossos comandos. Certamente estava ligado e funcional.", FirstAppear = 0.0f });
            Papers.Enqueue(new Line { StoryLine = "7/8. IBBM-PS esta completamente fora de si! Devemos suspender essas pesquisas agora!\nKleber foi atacado, ninguem vem nos ajudar. Nos prenderam aqui, parece que\nsabiam o que iria acontecer.", FirstAppear = 0.0f });
            Papers.Enqueue(new Line { StoryLine = "8/8. IBBM-PS e a tecnologia mais inovadora e relevante da ultima decada em seu segmento.\nIBBM tem planos para distribuicao em massa como computadores pessoais.", FirstAppear = 0.0f });
            Papers.Enqueue(new Line { StoryLine = "", FirstAppear = 0.0f });
        }

        private static void LoadChoiceLines()
        {
            ChoiceLines.Enqueue(new Line { StoryLine = "Juntei todos os trabalhos, eu me meti onde nao deveria. Quero sair daqui,\nmas meu dever diz que devo encontrar essas criancas. O que devo fazer?\nMe arriscar mais e procurar as vitimas? Ir embora e divulgar o que encontrei?", FirstAppear = 0.0f });
        }

        private static void LoadEnding1()
        {
            Ending1.Enqueue(new Line { StoryLine = "Eles estao aqui.", FirstAppear = 0.0f });
            Ending1.Enqueue(new Line { StoryLine = "Assim como eu. Esse e o meu fim.", FirstAppear = 0.0f });
            Ending1.Enqueue(new Line { StoryLine = "O mundo jamais sabera da verdade.", FirstAppear = 0.0f });
        }

        private static void LoadEnding2()
        {
            Ending2.Enqueue(new Line { StoryLine = "", FirstAppear = 0.0f });
            Ending2.Enqueue(new Line { StoryLine = "Ninguem sabera o real destino desses meninos. Desisti da minha busca apos\nvivenciar os acontecimentos.", FirstAppear = 0.0f });
            Ending2.Enqueue(new Line { StoryLine = "Levei os artigos as autoridades com o objetivo de reabrir o caso.\nNao fui escutado.", FirstAppear = 0.0f });
            Ending2.Enqueue(new Line { StoryLine = "Se passaram mais de 5 anos do caso, as vitimas certamente estao mortas\na esse ponto. Tudo que ganhei foi o repudio da familia da\nvitima e um titulo de louco.", FirstAppear = 0.0f });
        }

        public static void DrawStoryLine(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentDraw.FirstAppear == 0.0f)
            {
                currentDraw.FirstAppear = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            spriteBatch.DrawString(Art.Font, currentDraw.StoryLine, new Vector2(100, Game1.ScreenSize.Y / 2.0f), Color.White);

            if (currentDraw.FirstAppear + 0.1f <= gameTime.TotalGameTime.TotalSeconds)
            {
                StoryLines.Dequeue();
                if (StoryLines.Count > 0) currentDraw = StoryLines.Peek();
            }
        }

        public static void DrawPlayingLines(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentDraw.FirstAppear == 0.0f)
            {
                currentDraw.FirstAppear = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            spriteBatch.DrawString(Art.Font, currentDraw.StoryLine, new Vector2(100, Game1.ScreenSize.Y / 2.0f), Color.White);

            if (currentDraw.FirstAppear + 4.5f <= gameTime.TotalGameTime.TotalSeconds)
            {
                PlayingStory.Dequeue();
                if (PlayingStory.Count > 0)
                {
                    currentDraw = PlayingStory.Peek();
                }
                else currentDraw = BuildingStory.Peek();
            }
        }

        public static void DrawBuildingLines(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentDraw.FirstAppear == 0.0f)
            {
                currentDraw.FirstAppear = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            spriteBatch.DrawString(Art.Font, currentDraw.StoryLine, new Vector2(100, Game1.ScreenSize.Y / 2.0f), Color.White);

            if (currentDraw.FirstAppear + 2.5f <= gameTime.TotalGameTime.TotalSeconds)
            {
                BuildingStory.Dequeue();
                if (BuildingStory.Count > 0)
                {
                    currentDraw = BuildingStory.Peek();
                }
                else currentDraw = ChoiceLines.Peek();
            }
        }

        public static void DrawPaperLines(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentPaper.FirstAppear == 0.0f)
            {
                currentPaper.FirstAppear = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            spriteBatch.DrawString(Art.Font, currentPaper.StoryLine, new Vector2(100, Game1.ScreenSize.Y / 2.0f), Color.White);

            if (currentPaper.FirstAppear + 5f <= gameTime.TotalGameTime.TotalSeconds)
            {
                Papers.Dequeue();
                if (Papers.Count > 0)
                {
                    currentPaper = Papers.Peek();
                    if (Level.Name == "building1") Level.PapersRead1++;
                    if (Level.Name == "building2") Level.PapersRead2++;
                }

            }
        }

        public static void DrawLockedLine(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentLocked.FirstAppear == 0.0f)
            {
                currentLocked.FirstAppear = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            spriteBatch.DrawString(Art.Font, "Eu preciso encontrar 4 artigos para destrancar.", new Vector2(100, Game1.ScreenSize.Y / 2.0f), Color.White);

            if (currentLocked.FirstAppear + 5f <= gameTime.TotalGameTime.TotalSeconds)
            {
                currentLocked = new Line { StoryLine = "", FirstAppear = 0.0f };
                Level.OpenedLockedDoor = false;
            }
        }

        public static void DrawChoiceLine(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentDraw.FirstAppear == 0.0f)
            {
                currentDraw.FirstAppear = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            spriteBatch.DrawString(Art.Font, currentDraw.StoryLine, new Vector2(100, Game1.ScreenSize.Y / 2.0f), Color.White);

            if (currentDraw.FirstAppear + 10f <= gameTime.TotalGameTime.TotalSeconds)
            {
                ChoiceLines.Dequeue();
                if (ChoiceLines.Count > 0)
                {
                    currentDraw = ChoiceLines.Peek();
                }

            }
        }

        public static void DrawEnding1Lines(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentEnding1.FirstAppear == 0.0f)
            {
                currentEnding1.FirstAppear = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            spriteBatch.DrawString(Art.Font, currentEnding1.StoryLine, new Vector2(100, Game1.ScreenSize.Y / 2.0f), Color.White);

            if (currentEnding1.FirstAppear + 4.5f <= gameTime.TotalGameTime.TotalSeconds)
            {
                Ending1.Dequeue();
                if (Ending1.Count > 0)
                {
                    currentEnding1 = Ending1.Peek();
                }
            }
        }

        public static void DrawEnding2Lines(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentEnding2.FirstAppear == 0.0f)
            {
                currentEnding2.FirstAppear = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            spriteBatch.DrawString(Art.Font, currentEnding2.StoryLine, new Vector2(100, Game1.ScreenSize.Y / 2.0f), Color.White);

            if (currentEnding2.FirstAppear + 4.5f <= gameTime.TotalGameTime.TotalSeconds)
            {
                Ending2.Dequeue();
                if (Ending2.Count > 0)
                {
                    currentEnding2 = Ending2.Peek();
                }
            }
        }
        public static void DrawEnd(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Art.Pixel, Vector2.Zero, new Rectangle(0, 0, (int)Game1.ScreenSize.X, (int)Game1.ScreenSize.Y), Color.Black);
            spriteBatch.DrawString(Art.Font, "The end.", new Vector2(100, Game1.ScreenSize.Y / 2.0f), Color.White);
        }

    }
}