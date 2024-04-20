using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.SystemTests.Source.TextDemo;

public class TextDemoScreen : Screen
{
    public TextDemoScreen()
    {
        TextTheme theme = new()
        {
            Font = HFont.Load("Arial", 8),
            FontColour = Colour.White,
            FontScale = 1,
            HAlignment = HAlignment.Centred,
            VAlignment = VAlignment.Centred,
        };

        var entity = new Entity(1, 20, 20, 40, 40, Colour.Black);
        Add(entity);
        entity.Add(new TextGraphic(theme, entity.Size, "Hello"));
    }

    public override void Start()
    {
        base.Start();


        //TextGraphic hText;


        //textArea = new Rect(0, 200, 399, 199);
        //colour = Colour.Green;
        //font = "fuckup";
        //fontSize = 20;
        //Add(new Line(colour, 1, true, textArea));
        ////Add(new HText(HFont.Load(font, fontSize, FontStyle.Regular, true), textArea, "Times New Roman 20 centre aligned: abcdefghijklmnopqrstuvwxyz\n<u><bi>This text is underlined bold italic with <colour=0,0,0,255>black colour</colour> in the middle.</bi></u>", colour) { ExtraCharacterSpacing = -1, HAlignment = HAlignment.Centred, VAlignment = VAlignment.Centred });

        //textArea = new Rect(0, 400, 399, 199);
        //colour = Colour.Yellow;
        //font = "Helvetica";
        //fontSize = 20;
        //Add(new Line(colour, 1, true, textArea));
        ////Add(new HText(HFont.Load(font, fontSize, FontStyle.Regular, false), textArea, "Helvetica /<displaythis> 20 Right Aligned: a_b_c_d_e_f_g_h_i_j_k_l_m_n_o_p_q_r_s_t_u_v_w_x_y_z", colour) { HAlignment = HAlignment.Right });

        //textArea = new Rect(400, 0, 199, 149);
        //colour = Colour.White;
        //font = "Verdana";
        //fontSize = 12;
        //Add(new Line(colour, 1, true, textArea));
        ////Add(new HText(HFont.Load(font, fontSize, FontStyle.Regular, false), textArea, "Verdana Italic 12: <i>abcdefghijklmnopqrstuvwxyz</>", colour));

        //textArea = new Rect(600, 0, 199, 149);
        //colour = Colour.Orange;
        //font = "Futura";
        //fontSize = 8;
        //Add(new Line(colour, 1, true, textArea));
        ////Add(new HText(HFont.Load(font, fontSize, FontStyle.Regular, false), textArea, "Futura 8: abcdefghijklmnopqrstuvwxyz", colour));

        //textArea = new Rect(400, 150, 199, 149);
        //colour = Colour.Pink;
        //font = "Calibri";
        //fontSize = 14;
        //Add(new Line(colour, 1, true, textArea));
        ////Add(new HText(HFont.Load(font, fontSize, FontStyle.Regular, false), textArea, "Calibri 15 Fully Justified. The Quick Brown <size=20>Bear</size> jumped over the lazy dog & fox.\nThen he ate some salmon. The end.", colour) { HAlignment = HAlignment.Full, VAlignment = VAlignment.Full });

        //textArea = new Rect(401, 301, 398, 298);
        //colour = Colour.Black;
        //font = "Franklin Gothic Medium";
        //fontSize = 10;
        //Add(new Line(colour, 1, true, textArea));
        //Add(new HText(HFont.Load(font, fontSize, FontStyle.Regular, true), textArea.Grow(-1), @"""I think he's an old dear,"" said Susan." + "\n" + @"""Oh, come off it!"" said Edmund, who was tired and pretending not to be tired, which always made him bad - tempered. ""Don't go on talking like that.""" + "\n" + @"""Like what?"" said Susan; ""and anyway, it's time you were in bed.""" + "\n" + @"""Trying to talk like Mother,"" said Edmund. ""And who are you to say when I'm to go to bed? <font=Comic Sans MS,15,0,0>Go to bed yourself.</>""" + "\n" + @"""Hadn't we all better go to bed?"" said Lucy. ""There's sure to be a row if we're heard talking here.""" + "\n" + @"""No there won't,"" said Peter. ""I tell you this is the sort of house where no one's going to mind what we do. Anyway, they won't hear us. It's about ten minutes' walk from here down to that dining room, and any amount of stairs and passages in between.""" + "\n" + @"""What's that noise?"" said Lucy suddenly.It was a far larger house than she had ever been in before and the thought of all those long passages and rows of doors leading into empty rooms was beginning to make her feel a little creepy." + "\n" + @"""It's only a <size=16><colour=255,255,0,255>bird</></>, silly,"" said Edmund.", colour) { HAlignment = HAlignment.Full, VAlignment = VAlignment.Full });
        //GridLayout g = new(0, new Rect(800, 600), 1, 2);
        //g.Margin = 5;
        //g.ColumnSpacing = 5;
        //g.RowSpacing = 5;
        //Add(g);

        //GridLayout gLeft = new(0, Rect.EmptyRect, 3, 1);
        //gLeft.Margin = 5;
        //gLeft.ColumnSpacing = 5;
        //gLeft.RowSpacing = 5;
        //g.Add(gLeft);

        //GridLayout gRight = new(0, Rect.EmptyRect, 2, 1);
        //gRight.Margin = 5;
        //gRight.ColumnSpacing = 5;
        //gRight.RowSpacing = 5;
        //g.Add(gRight);

        //GridLayout gTopRight = new GridLayout(0, Rect.EmptyRect, 2, 2);
        //gTopRight.Margin = 5;
        //gTopRight.ColumnSpacing = 5;
        //gTopRight.RowSpacing = 5;
        //gRight.Add(gTopRight);

        //text = "Arial 40 Bold: <b>Here is some <colour=(255,0,0,255)>red</colour> text</b>";
        //hText = new TextGraphic(HFont.Load("Arial", 40, FontStyle.Regular, false), Colour.Blue, Rect.EmptyRect, text) { Multiline = true, Underline = true, UnderlineThickness = 2, UnderlineOffset = -8 };

        //gLeft.Add(hText);
        //gLeft.Add(new Image(Colour.Yellow, Rect.UnitRect));
        //gLeft.Add(new Image(Colour.Yellow, Rect.UnitRect));
        //gTopRight.Add(new Image(Colour.Yellow, Rect.UnitRect));
        //gTopRight.Add(new Image(Colour.Yellow, Rect.UnitRect));
        //gTopRight.Add(new Image(Colour.Yellow, Rect.UnitRect));

        //text = "Didot 12: a b c d e f g h i j k l m n o p q r s t u v w x y z";
        //hText = new TextGraphic(HFont.Load("Didot.ttf", 12, FontStyle.Regular, false), Colour.Silver, Rect.EmptyRect, text) { Multiline = false, UseCommandTags = false };
        //gTopRight.Add(hText);
        //gRight.Add(new Image(Colour.Yellow, Rect.UnitRect));

        //ICamera camera = new Camera(1, GP.DefaultClientSize.ToRect(), 1, 1)
        //{
        //    BackgroundColour = Colour.CornflowerBlue
        //};
        //Add(camera);

        var b = new Button(1, new Rect(730, 550, 60, 40), Colour.White, GV.Theme, () => Engine.Scene = new MenuScreen());
        b.Add(new TextGraphic(GV.MainFont, Colour.Black, new Rect(60, 40), "Return") { HAlignment = HAlignment.Centred, VAlignment = VAlignment.Centred });
        Add(b);
    }
}