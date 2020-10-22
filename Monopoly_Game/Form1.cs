using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly_Game
{
    public partial class Form1 : Form
    {
        // variables
        int dice_throw1, dice_throw2;
        bool rollAgain = false;

        bool comingOutOfJale = false;
        bool player1Jail = false;
        bool chance1Jail = false;
        bool player2Jail = false;
        bool chance2Jail = false;
        bool player3Jail = false;
        bool chance3Jail = false;
        bool player4Jail = false;
        bool chance4Jail = false;

        bool p1GameOver = false;
        bool p2GameOver = false;
        bool p3GameOver = false;
        bool p4GameOver = false;

        int jailTimeP1 = 0;
        int jailTimeP2 = 0;
        int jailTimeP3 = 0;
        int jailTimeP4 = 0;
        int turn = 1;
        // keeps truck of player position in a background
        int player1Position = 0;
        int player2Position = 0;
        int player3Position = 0;
        int player4Position = 0;

        int player1Money = 0;
        int player2Money = 0;
        int player3Money = 0;
        int player4Money = 0;
        // tracks how many properties player owns
        int player1Property = 0;
        int player2Property = 0;
        int player3Property = 0;
        int player4Property = 0;
        // -1 can't buy the square // 0 square can be bought // 1 square is owned by someone
        int[] properties = new int[40] {-1, 0, -1, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, 0, 0, 0, 0, -1, 0, 0, -1, 0, -1, 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, -1, 0};
        int[] prices = new int[40] {0, 60, 0, 60, 0, 200, 100, 0, 100, 120, 0, 140, 150, 140, 160, 200, 180, 0, 180, 200, 0, 220, 0, 220, 240, 200, 260, 260, 150, 280, 0, 300, 300, 0, 320, 200, 0, 350, 0, 400};
        int[] rent = new int[40] {0, 2, 0, 4, 0, 25, 6, 0, 6, 8, 0, 10, 15, 10, 12, 25, 14, 0, 14, 16, 0, 18, 0, 18, 20, 25, 22, 22, 15, 22, 0, 26, 26, 0, 28, 25, 0, 35, 0, 50};
        int countGameOver = 0;
        //bool start = false; // THIS WILL HELP TO ADD TRANSPARENCY TO PLAYER PIECES IF I DECIDE TO USE IT LATER

        public Form1()
        {
            InitializeComponent();
        }

        // initializing properties to 0
        public void InitializeProperty()
        {
            labelP1Property.Text = player1Property.ToString();
            labelP2Property.Text = player2Property.ToString();
            labelP3Property.Text = player3Property.ToString();
            labelP4Property.Text = player4Property.ToString();
        }

        // initializing money to 1500
        public void InitializeMoney()   
        {
            player1Money += 1500;
            player2Money += 1500;
            player3Money += 1500;
            player4Money += 1500;
            labelP1Money.Text = "£" + player1Money.ToString();
            labelP2Money.Text = "£" + player2Money.ToString();
            labelP3Money.Text = "£" + player3Money.ToString();
            labelP4Money.Text = "£" + player4Money.ToString();
        }

        // returns properties as available to be bought once player loses
        public void returnPropertiesP1()
        {
            if (p1GameOver)
            {
                countGameOver += 1;
                for (int i = 0; i < 40; i++)
                {
                    if (properties[i] == 1)
                        properties[i] = 0;
                }
            }
        }

        public void returnPropertiesP2() {
            if (p2GameOver)
            {
                countGameOver += 1;
                for (int i = 0; i < 40; i++)
                {
                    if (properties[i] == 2)
                        properties[i] = 0;
                }
            }
        }

        public void returnPropertiesP3() {
            if (p3GameOver)
            {
                countGameOver += 1;
                for (int i = 0; i < 40; i++)
                {
                    if (properties[i] == 3)
                        properties[i] = 0;
                }
            }
        }

        public void returnPropertiesP4() {
            if (p4GameOver)
            {
                countGameOver += 1;
                for (int i = 0; i < 40; i++)
                {
                    if (properties[i] == 4)
                        properties[i] = 0;
                }
            }
        }

        // positions players on a Start square
        public void StartPlayerPieces()
        {
            labelPlayer1Piece.Visible = true;
            labelPlayer2Piece.Visible = true;
            labelPlayer3Piece.Visible = true;
            labelPlayer4Piece.Visible = true;
            labelPlayer1Piece.Top = pictureBoxStart.Top + 8;
            labelPlayer1Piece.Left = pictureBoxStart.Left + 8;
            labelPlayer2Piece.Top = pictureBoxStart.Top + 8;
            labelPlayer2Piece.Left = pictureBoxStart.Left + 70;
            labelPlayer3Piece.Top = pictureBoxStart.Top + 65;
            labelPlayer3Piece.Left = pictureBoxStart.Left + 8;
            labelPlayer4Piece.Top = pictureBoxStart.Top + 65;
            labelPlayer4Piece.Left = pictureBoxStart.Left + 70;
        }

        // giving player1  specific space on all the squares on boards + added logic when on special squares
        public void player1Movement()
        {
            if (player1Position == 0)
            {
                labelPlayer1Piece.Top = pictureBoxStart.Top + 8;
                labelPlayer1Piece.Left = pictureBoxStart.Left + 8;
            }
            if (player1Position == 1)
            {
                labelPlayer1Piece.Top = pictureBoxOldKent.Top + 5;
                labelPlayer1Piece.Left = pictureBoxOldKent.Left + 5;
            }
            if (player1Position == 2)
            {
                labelPlayer1Piece.Top = pictureBoxChest1.Top + 5;
                labelPlayer1Piece.Left = pictureBoxChest1.Left + 5;
                currentSquarePlayer1();
                communityChestPlayer1();
            }
            if (player1Position == 3)
            {
                labelPlayer1Piece.Top = pictureBoxWhiteChapel.Top + 5;
                labelPlayer1Piece.Left = pictureBoxWhiteChapel.Left + 5;
            }
            if (player1Position == 4) // adding the logic in for special squares
            {
                labelPlayer1Piece.Top = pictureBoxIncomeTax.Top + 5;
                labelPlayer1Piece.Left = pictureBoxIncomeTax.Left + 5;
                currentSquarePlayer1();
                if (player1Money < 200)
                {
                    MessageBox.Show("PLAYER 1 - PAY TAX £200!!", "INCOME TAX");
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PAY INCOME TAX");
                    MessageBox.Show("PLAYER 1 - GAME OVER");
                    player1Money = 0;
                    labelP1Property.Visible = false;
                    labelP1Money.Visible = false;
                    labelPlayer1Piece.Visible = false;
                    labelMoney1.Text = "Game";
                    labelPlayer1Property.Text = "Over";
                    p1GameOver = true;
                    returnPropertiesP1();
                }
                else
                {
                    player1Money -= 200;
                    MessageBox.Show("PLAYER 1 - PAY TAX £200!!", "INCOME TAX");
                    labelP1Money.Text = "£" + player1Money.ToString();
                }
            }
            if (player1Position == 5)
            {
                labelPlayer1Piece.Top = pictureBoxTrain1.Top + 5;
                labelPlayer1Piece.Left = pictureBoxTrain1.Left + 5;
            }
            if (player1Position == 6)
            {
                labelPlayer1Piece.Top = pictureBoxTheAngel.Top + 5;
                labelPlayer1Piece.Left = pictureBoxTheAngel.Left + 5;
            }
            if (player1Position == 7)
            {
                labelPlayer1Piece.Top = pictureBoxChance1.Top + 5;
                labelPlayer1Piece.Left = pictureBoxChance1.Left + 5;
                currentSquarePlayer1();
                chanceCardPlayer1();
            }
            if (player1Position == 8)
            {
                labelPlayer1Piece.Top = pictureBoxEuston.Top + 5;
                labelPlayer1Piece.Left = pictureBoxEuston.Left + 5;
            }
            if (player1Position == 9)
            {
                labelPlayer1Piece.Top = pictureBoxPentonville.Top + 5;
                labelPlayer1Piece.Left = pictureBoxPentonville.Left + 5;
            }
            if ((player1Position == 10) && (!player1Jail))
            {
                labelPlayer1Piece.Top = pictureBoxJail.Top + 2;
                labelPlayer1Piece.Left = pictureBoxJail.Left + 3;
            }
            if (player1Position == 11)
            {
                labelPlayer1Piece.Top = pictureBoxPallMall.Top + 3;
                labelPlayer1Piece.Left = pictureBoxPallMall.Left + 5;
            }
            if (player1Position == 12)
            {
                labelPlayer1Piece.Top = pictureBoxElectric.Top + 3;
                labelPlayer1Piece.Left = pictureBoxElectric.Left + 5;
            }
            if (player1Position == 13)
            {
                labelPlayer1Piece.Top = pictureBoxWhitehall.Top + 3;
                labelPlayer1Piece.Left = pictureBoxWhitehall.Left + 5;
            }
            if (player1Position == 14)
            {
                labelPlayer1Piece.Top = pictureBoxNorthumrl.Top + 3;
                labelPlayer1Piece.Left = pictureBoxNorthumrl.Left + 5;
            }
            if (player1Position == 15)
            {
                labelPlayer1Piece.Top = pictureBoxTrain2.Top + 3;
                labelPlayer1Piece.Left = pictureBoxTrain2.Left + 5;
            }
            if (player1Position == 16)
            {
                labelPlayer1Piece.Top = pictureBoxBow.Top + 2;
                labelPlayer1Piece.Left = pictureBoxBow.Left + 5;
            }
            if (player1Position == 17)
            {
                labelPlayer1Piece.Top = pictureBoxChest2.Top + 3;
                labelPlayer1Piece.Left = pictureBoxChest2.Left + 5;
                currentSquarePlayer1();
                communityChestPlayer1();
            }
            if (player1Position == 18)
            {
                labelPlayer1Piece.Top = pictureBoxMarlborough.Top + 3;
                labelPlayer1Piece.Left = pictureBoxMarlborough.Left + 5;
            }
            if (player1Position == 19)
            {
                labelPlayer1Piece.Top = pictureBoxVine.Top + 3;
                labelPlayer1Piece.Left = pictureBoxVine.Left + 5;
            }
            if (player1Position == 20)
            {
                labelPlayer1Piece.Top = pictureBoxFreeParking.Top + 8;
                labelPlayer1Piece.Left = pictureBoxFreeParking.Left + 8;
            }
            if (player1Position == 21)
            {
                labelPlayer1Piece.Top = pictureBoxStrand.Top + 5;
                labelPlayer1Piece.Left = pictureBoxStrand.Left + 5;
            }
            if (player1Position == 22)
            {
                labelPlayer1Piece.Top = pictureBoxChance2.Top + 5;
                labelPlayer1Piece.Left = pictureBoxChance2.Left + 5;
                currentSquarePlayer1();
                chanceCardPlayer1();
            }
            if (player1Position == 23)
            {
                labelPlayer1Piece.Top = pictureBoxFleet.Top + 5;
                labelPlayer1Piece.Left = pictureBoxFleet.Left + 5;
            }
            if (player1Position == 24)
            {
                labelPlayer1Piece.Top = pictureBoxTrafalgar.Top + 5;
                labelPlayer1Piece.Left = pictureBoxTrafalgar.Left + 5;
            }
            if (player1Position == 25)
            {
                labelPlayer1Piece.Top = pictureBoxTrain3.Top + 5;
                labelPlayer1Piece.Left = pictureBoxTrain3.Left + 5;
            }
            if (player1Position == 26)
            {
                labelPlayer1Piece.Top = pictureBoxLeicester.Top + 5;
                labelPlayer1Piece.Left = pictureBoxLeicester.Left + 5;
            }
            if (player1Position == 27)
            {
                labelPlayer1Piece.Top = pictureBoxCoventry.Top + 5;
                labelPlayer1Piece.Left = pictureBoxCoventry.Left + 5;
            }
            if (player1Position == 28)
            {
                labelPlayer1Piece.Top = pictureBoxWaterWorks.Top + 5;
                labelPlayer1Piece.Left = pictureBoxWaterWorks.Left + 5;
            }
            if (player1Position == 29)
            {
                labelPlayer1Piece.Top = pictureBoxPicadilly.Top + 5;
                labelPlayer1Piece.Left = pictureBoxPicadilly.Left + 5;
            }
            if ((player1Position == 30) && (!chance1Jail))
            {
                labelPlayer1Piece.Top = pictureBoxGoToJail.Top + 8;
                labelPlayer1Piece.Left = pictureBoxGoToJail.Left + 8;
            }
            if (player1Position == 31)
            {
                labelPlayer1Piece.Top = pictureBoxRegent.Top + 3;
                labelPlayer1Piece.Left = pictureBoxRegent.Left + 5;
            }
            if (player1Position == 32)
            {
                labelPlayer1Piece.Top = pictureBoxOxford.Top + 3;
                labelPlayer1Piece.Left = pictureBoxOxford.Left + 5;
            }
            if (player1Position == 33)
            {
                labelPlayer1Piece.Top = pictureBoxChest3.Top + 3;
                labelPlayer1Piece.Left = pictureBoxChest3.Left + 5;
                currentSquarePlayer1();
                communityChestPlayer1();
            }
            if (player1Position == 34)
            {
                labelPlayer1Piece.Top = pictureBoxBond.Top + 3;
                labelPlayer1Piece.Left = pictureBoxBond.Left + 5;
            }
            if (player1Position == 35)
            {
                labelPlayer1Piece.Top = pictureBoxTrain4.Top + 3;
                labelPlayer1Piece.Left = pictureBoxTrain4.Left + 5;
            }
            if (player1Position == 36)
            {
                labelPlayer1Piece.Top = pictureBoxChance3.Top + 3;
                labelPlayer1Piece.Left = pictureBoxChance3.Left + 5;
                currentSquarePlayer1();
                chanceCardPlayer1();
            }
            if (player1Position == 37)
            {
                labelPlayer1Piece.Top = pictureBoxParkLane.Top + 3;
                labelPlayer1Piece.Left = pictureBoxParkLane.Left + 5;
            }
            if (player1Position == 38) // adding logic for special squares
            {
                labelPlayer1Piece.Top = pictureBoxSuperTax.Top + 3;
                labelPlayer1Piece.Left = pictureBoxSuperTax.Left + 5;
                currentSquarePlayer1();
                if (player1Money < 100)
                {
                    MessageBox.Show("PLAYER 1 - PAY TAX £100!!", "SUPER TAX");
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PAY SUPER TAX");
                    MessageBox.Show("PLAYER 1 - GAME OVER");
                    player1Money = 0;
                    labelP1Property.Visible = false;
                    labelP1Money.Visible = false;
                    labelPlayer1Piece.Visible = false;
                    labelMoney1.Text = "Game";
                    labelPlayer1Property.Text = "Over";
                    p1GameOver = true;
                    returnPropertiesP1();
                }
                else
                {
                    player1Money -= 100;
                    MessageBox.Show("PLAYER 1 - PAY TAX £100!!", "SUPER TAX");
                    labelP1Money.Text = "£" + player1Money.ToString();
                }
            }
            if (player1Position == 39)
            {
                labelPlayer1Piece.Top = pictureBoxMayfair.Top + 3;
                labelPlayer1Piece.Left = pictureBoxMayfair.Left + 5;
            }
        }

        public void player2Movement()
        {
            if (player2Position == 0)
            {
                labelPlayer2Piece.Top = pictureBoxStart.Top + 8;
                labelPlayer2Piece.Left = pictureBoxStart.Left + 70; 
            }
            if (player2Position == 1)
            {
                labelPlayer2Piece.Top = pictureBoxOldKent.Top + 5;
                labelPlayer2Piece.Left = pictureBoxOldKent.Left + 39;
            }
            if (player2Position == 2)
            {
                labelPlayer2Piece.Top = pictureBoxChest1.Top + 5;
                labelPlayer2Piece.Left = pictureBoxChest1.Left + 40;
                currentSquarePlayer2();
                communityChestPlayer2();
            }
            if (player2Position == 3)
            {
                labelPlayer2Piece.Top = pictureBoxWhiteChapel.Top + 5;
                labelPlayer2Piece.Left = pictureBoxWhiteChapel.Left + 40;
            }
            if (player2Position == 4) // added logic for special square
            {
                labelPlayer2Piece.Top = pictureBoxIncomeTax.Top + 5;
                labelPlayer2Piece.Left = pictureBoxIncomeTax.Left + 40;
                currentSquarePlayer2();
                if (player2Money < 200)
                {
                    MessageBox.Show("PLAYER 2 - PAY TAX £200!!", "INCOME TAX");
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PAY INCOME TAX");
                    MessageBox.Show("PLAYER 2 - GAME OVER");
                    player2Money = 0;
                    labelP2Money.Visible = false;
                    labelP2Property.Visible = false;
                    labelPlayer2Piece.Visible = false;
                    labelMoney2.Text = "Game";
                    labelPlayer2Property.Text = "Over";
                    p2GameOver = true;
                    returnPropertiesP2();
                }
                else
                {
                    player2Money -= 200;
                    MessageBox.Show("PLAYER 2 - PAY TAX £200!!", "INCOME TAX");
                    labelP2Money.Text = "£" + player2Money.ToString();
                }
            }
            if (player2Position == 5)
            {
                labelPlayer2Piece.Top = pictureBoxTrain1.Top + 5;
                labelPlayer2Piece.Left = pictureBoxTrain1.Left + 40;
            }
            if (player2Position == 6)
            {
                labelPlayer2Piece.Top = pictureBoxTheAngel.Top + 5;
                labelPlayer2Piece.Left = pictureBoxTheAngel.Left + 40;
            }
            if (player2Position == 7)
            {
                labelPlayer2Piece.Top = pictureBoxChance1.Top + 5;
                labelPlayer2Piece.Left = pictureBoxChance1.Left + 40;
                currentSquarePlayer2();
                chanceCardPlayer2();
            }
            if (player2Position == 8)
            {
                labelPlayer2Piece.Top = pictureBoxEuston.Top + 5;
                labelPlayer2Piece.Left = pictureBoxEuston.Left + 40;
            }
            if (player2Position == 9)
            {
                labelPlayer2Piece.Top = pictureBoxPentonville.Top + 5;
                labelPlayer2Piece.Left = pictureBoxPentonville.Left + 40;
            }
            if ((player2Position == 10) && (!player2Jail))
            {
                labelPlayer2Piece.Top = pictureBoxJail.Top + 35;
                labelPlayer2Piece.Left = pictureBoxJail.Left + 2;
            }
            if (player2Position == 11)
            {
                labelPlayer2Piece.Top = pictureBoxPallMall.Top + 3;
                labelPlayer2Piece.Left = pictureBoxPallMall.Left + 70;
            }
            if (player2Position == 12)
            {
                labelPlayer2Piece.Top = pictureBoxElectric.Top + 3;
                labelPlayer2Piece.Left = pictureBoxElectric.Left + 70;
            }
            if (player2Position == 13)
            {
                labelPlayer2Piece.Top = pictureBoxWhitehall.Top + 3;
                labelPlayer2Piece.Left = pictureBoxWhitehall.Left + 70;
            }
            if (player2Position == 14)
            {
                labelPlayer2Piece.Top = pictureBoxNorthumrl.Top + 3;
                labelPlayer2Piece.Left = pictureBoxNorthumrl.Left + 70;
            }
            if (player2Position == 15)
            {
                labelPlayer2Piece.Top = pictureBoxTrain2.Top + 3;
                labelPlayer2Piece.Left = pictureBoxTrain2.Left + 70;
            }
            if (player2Position == 16)
            {
                labelPlayer2Piece.Top = pictureBoxBow.Top + 3;
                labelPlayer2Piece.Left = pictureBoxBow.Left + 70;
            }
            if (player2Position == 17)
            {
                labelPlayer2Piece.Top = pictureBoxChest2.Top + 3;
                labelPlayer2Piece.Left = pictureBoxChest2.Left + 70;
                currentSquarePlayer2();
                communityChestPlayer2();
            }
            if (player2Position == 18)
            {
                labelPlayer2Piece.Top = pictureBoxMarlborough.Top + 3;
                labelPlayer2Piece.Left = pictureBoxMarlborough.Left + 70;
            }
            if (player2Position == 19)
            {
                labelPlayer2Piece.Top = pictureBoxVine.Top + 3;
                labelPlayer2Piece.Left = pictureBoxVine.Left + 70;
            }
            if (player2Position == 20)
            {
                labelPlayer2Piece.Top = pictureBoxFreeParking.Top + 8;
                labelPlayer2Piece.Left = pictureBoxFreeParking.Left + 70;
            }
            if (player2Position == 21)
            {
                labelPlayer2Piece.Top = pictureBoxStrand.Top + 5;
                labelPlayer2Piece.Left = pictureBoxStrand.Left + 40;
            }
            if (player2Position == 22)
            {
                labelPlayer2Piece.Top = pictureBoxChance2.Top + 5;
                labelPlayer2Piece.Left = pictureBoxChance2.Left + 40;
                currentSquarePlayer2();
                chanceCardPlayer2();
            }
            if (player2Position == 23)
            {
                labelPlayer2Piece.Top = pictureBoxFleet.Top + 5;
                labelPlayer2Piece.Left = pictureBoxFleet.Left + 40;
            }
            if (player2Position == 24)
            {
                labelPlayer2Piece.Top = pictureBoxTrafalgar.Top + 5;
                labelPlayer2Piece.Left = pictureBoxTrafalgar.Left + 40;
            }
            if (player2Position == 25)
            {
                labelPlayer2Piece.Top = pictureBoxTrain3.Top + 5;
                labelPlayer2Piece.Left = pictureBoxTrain3.Left + 40;
            }
            if (player2Position == 26)
            {
                labelPlayer2Piece.Top = pictureBoxLeicester.Top + 5;
                labelPlayer2Piece.Left = pictureBoxLeicester.Left + 40;
            }
            if (player2Position == 27)
            {
                labelPlayer2Piece.Top = pictureBoxCoventry.Top + 5;
                labelPlayer2Piece.Left = pictureBoxCoventry.Left + 40;
            }
            if (player2Position == 28)
            {
                labelPlayer2Piece.Top = pictureBoxWaterWorks.Top + 5;
                labelPlayer2Piece.Left = pictureBoxWaterWorks.Left + 40;
            }
            if (player2Position == 29)
            {
                labelPlayer2Piece.Top = pictureBoxPicadilly.Top + 5;
                labelPlayer2Piece.Left = pictureBoxPicadilly.Left + 40;
            }
            if ((player2Position == 30) && (!chance2Jail))
            {
                labelPlayer2Piece.Top = pictureBoxGoToJail.Top + 8;
                labelPlayer2Piece.Left = pictureBoxGoToJail.Left + 70;
            }
            if (player2Position == 31)
            {
                labelPlayer2Piece.Top = pictureBoxRegent.Top + 3;
                labelPlayer2Piece.Left = pictureBoxRegent.Left + 70;
            }
            if (player2Position == 32)
            {
                labelPlayer2Piece.Top = pictureBoxOxford.Top + 3;
                labelPlayer2Piece.Left = pictureBoxOxford.Left + 70;
            }
            if (player2Position == 33)
            {
                labelPlayer2Piece.Top = pictureBoxChest3.Top + 3;
                labelPlayer2Piece.Left = pictureBoxChest3.Left + 70;
                currentSquarePlayer2();
                communityChestPlayer2();
            }
            if (player2Position == 34)
            {
                labelPlayer2Piece.Top = pictureBoxBond.Top + 3;
                labelPlayer2Piece.Left = pictureBoxBond.Left + 70;
            }
            if (player2Position == 35)
            {
                labelPlayer2Piece.Top = pictureBoxTrain4.Top + 3;
                labelPlayer2Piece.Left = pictureBoxTrain4.Left + 70;
            }
            if (player2Position == 36)
            {
                labelPlayer2Piece.Top = pictureBoxChance3.Top + 3;
                labelPlayer2Piece.Left = pictureBoxChance3.Left + 70;
                currentSquarePlayer2();
                chanceCardPlayer2();
            }
            if (player2Position == 37)
            {
                labelPlayer2Piece.Top = pictureBoxParkLane.Top + 3;
                labelPlayer2Piece.Left = pictureBoxParkLane.Left + 70;
            }
            if (player2Position == 38) // added logic for special squares
            {
                labelPlayer2Piece.Top = pictureBoxSuperTax.Top + 3;
                labelPlayer2Piece.Left = pictureBoxSuperTax.Left + 70;
                currentSquarePlayer2();
                if (player2Money < 100)
                {
                    MessageBox.Show("PLAYER 2 - PAY TAX £100!!", "SUPER TAX");
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PAY SUPER TAX");
                    MessageBox.Show("PLAYER 2 - GAME OVER");
                    player2Money = 0;
                    labelP2Money.Visible = false;
                    labelP2Property.Visible = false;
                    labelPlayer2Piece.Visible = false;
                    labelMoney2.Text = "Game";
                    labelPlayer2Property.Text = "Over";
                    p2GameOver = true;
                    returnPropertiesP2();
                }
                else
                {
                    player2Money -= 100;
                    MessageBox.Show("PLAYER 2 - PAY TAX £100!!", "SUPER TAX");
                    labelP2Money.Text = "£" + player2Money.ToString();
                }
            }
            if (player2Position == 39)
            {
                labelPlayer2Piece.Top = pictureBoxMayfair.Top + 3;
                labelPlayer2Piece.Left = pictureBoxMayfair.Left + 70;
            }
        }

        public void player3Movement()
        {
            if (player3Position == 0)
            {
                labelPlayer3Piece.Top = pictureBoxStart.Top + 65;
                labelPlayer3Piece.Left = pictureBoxStart.Left + 8;
            }
            if (player3Position == 1)
            {
                labelPlayer3Piece.Top = pictureBoxOldKent.Top + 65;
                labelPlayer3Piece.Left = pictureBoxOldKent.Left + 5;
            }
            if (player3Position == 2)
            {
                labelPlayer3Piece.Top = pictureBoxChest1.Top + 65;
                labelPlayer3Piece.Left = pictureBoxChest1.Left + 5;
                currentSquarePlayer3();
                communityChestPlayer3();
            }
            if (player3Position == 3)
            {
                labelPlayer3Piece.Top = pictureBoxWhiteChapel.Top + 65;
                labelPlayer3Piece.Left = pictureBoxWhiteChapel.Left + 5;
            }
            if (player3Position == 4) // added logic for special squares
            {
                labelPlayer3Piece.Top = pictureBoxIncomeTax.Top + 65;
                labelPlayer3Piece.Left = pictureBoxIncomeTax.Left + 5;
                currentSquarePlayer3();
                if (player3Money < 200)
                {
                    MessageBox.Show("PLAYER 3 - PAY TAX £200!!", "INCOME TAX");
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PAY INCOME TAX");
                    MessageBox.Show("PLAYER 3 - GAME OVER");
                    player3Money = 0;
                    labelP3Money.Visible = false;
                    labelP3Property.Visible = false;
                    labelPlayer3Piece.Visible = false;
                    labelMoney3.Text = "Game";
                    labelPlayer3Property.Text = "Over";
                    p3GameOver = true;
                    returnPropertiesP3();
                }
                else
                {
                    player3Money -= 200;
                    MessageBox.Show("PLAYER 3 - PAY TAX £200!!", "INCOME TAX");
                    labelP3Money.Text = "£" + player3Money.ToString();
                }
            }
            if (player3Position == 5)
            {
                labelPlayer3Piece.Top = pictureBoxTrain1.Top + 65;
                labelPlayer3Piece.Left = pictureBoxTrain1.Left + 5;
            }
            if (player3Position == 6)
            {
                labelPlayer3Piece.Top = pictureBoxTheAngel.Top + 65;
                labelPlayer3Piece.Left = pictureBoxTheAngel.Left + 5;
            }
            if (player3Position == 7)
            {
                labelPlayer3Piece.Top = pictureBoxChance1.Top + 65;
                labelPlayer3Piece.Left = pictureBoxChance1.Left + 5;
                currentSquarePlayer3();
                chanceCardPlayer3();
            }
            if (player3Position == 8)
            {
                labelPlayer3Piece.Top = pictureBoxEuston.Top + 65;
                labelPlayer3Piece.Left = pictureBoxEuston.Left + 5;
            }
            if (player3Position == 9)
            {
                labelPlayer3Piece.Top = pictureBoxPentonville.Top + 65;
                labelPlayer3Piece.Left = pictureBoxPentonville.Left + 5;
            }
            if ((player3Position == 10) && (!player3Jail))
            {
                labelPlayer3Piece.Top = pictureBoxJail.Top + 69;
                labelPlayer3Piece.Left = pictureBoxJail.Left + 2;
            }
            if (player3Position == 11)
            {
                labelPlayer3Piece.Top = pictureBoxPallMall.Top + 33;
                labelPlayer3Piece.Left = pictureBoxPallMall.Left + 5;
            }
            if (player3Position == 12)
            {
                labelPlayer3Piece.Top = pictureBoxElectric.Top + 33;
                labelPlayer3Piece.Left = pictureBoxElectric.Left + 5;
            }
            if (player3Position == 13)
            {
                labelPlayer3Piece.Top = pictureBoxWhitehall.Top + 33;
                labelPlayer3Piece.Left = pictureBoxWhitehall.Left + 5;
            }
            if (player3Position == 14)
            {
                labelPlayer3Piece.Top = pictureBoxNorthumrl.Top + 33;
                labelPlayer3Piece.Left = pictureBoxNorthumrl.Left + 5;
            }
            if (player3Position == 15)
            {
                labelPlayer3Piece.Top = pictureBoxTrain2.Top + 33;
                labelPlayer3Piece.Left = pictureBoxTrain2.Left + 5;
            }
            if (player3Position == 16)
            {
                labelPlayer3Piece.Top = pictureBoxBow.Top + 33;
                labelPlayer3Piece.Left = pictureBoxBow.Left + 5;
            }
            if (player3Position == 17)
            {
                labelPlayer3Piece.Top = pictureBoxChest2.Top + 33;
                labelPlayer3Piece.Left = pictureBoxChest2.Left + 5;
                currentSquarePlayer3();
                communityChestPlayer3();
            }
            if (player3Position == 18)
            {
                labelPlayer3Piece.Top = pictureBoxMarlborough.Top + 33;
                labelPlayer3Piece.Left = pictureBoxMarlborough.Left + 5;
            }
            if (player3Position == 19)
            {
                labelPlayer3Piece.Top = pictureBoxVine.Top + 33; 
                labelPlayer3Piece.Left = pictureBoxVine.Left + 5;
            }
            if (player3Position == 20)
            {
                labelPlayer3Piece.Top = pictureBoxFreeParking.Top + 65;
                labelPlayer3Piece.Left = pictureBoxFreeParking.Left + 8;
            }
            if (player3Position == 21)
            {
                labelPlayer3Piece.Top = pictureBoxStrand.Top + 65; 
                labelPlayer3Piece.Left = pictureBoxStrand.Left + 5;
            }
            if (player3Position == 22)
            {
                labelPlayer3Piece.Top = pictureBoxChance2.Top + 65;
                labelPlayer3Piece.Left = pictureBoxChance2.Left + 5;
                currentSquarePlayer3();
                chanceCardPlayer3();
            }
            if (player3Position == 23)
            {
                labelPlayer3Piece.Top = pictureBoxFleet.Top + 65;
                labelPlayer3Piece.Left = pictureBoxFleet.Left + 5;
            }
            if (player3Position == 24)
            {
                labelPlayer3Piece.Top = pictureBoxTrafalgar.Top + 65;
                labelPlayer3Piece.Left = pictureBoxTrafalgar.Left + 5;
            }
            if (player3Position == 25)
            {
                labelPlayer3Piece.Top = pictureBoxTrain3.Top + 65;
                labelPlayer3Piece.Left = pictureBoxTrain3.Left + 5;
            }
            if (player3Position == 26)
            {
                labelPlayer3Piece.Top = pictureBoxLeicester.Top + 65;
                labelPlayer3Piece.Left = pictureBoxLeicester.Left + 5;
            }
            if (player3Position == 27)
            {
                labelPlayer3Piece.Top = pictureBoxCoventry.Top + 65;
                labelPlayer3Piece.Left = pictureBoxCoventry.Left + 5;
            }
            if (player3Position == 28)
            {
                labelPlayer3Piece.Top = pictureBoxWaterWorks.Top + 65;
                labelPlayer3Piece.Left = pictureBoxWaterWorks.Left + 5;
            }
            if (player3Position == 29)
            {
                labelPlayer3Piece.Top = pictureBoxPicadilly.Top + 65;
                labelPlayer3Piece.Left = pictureBoxPicadilly.Left + 5;
            }
            if ((player3Position == 30) && (!chance3Jail))
            {
                labelPlayer3Piece.Top = pictureBoxGoToJail.Top + 65;
                labelPlayer3Piece.Left = pictureBoxGoToJail.Left + 8;
            }
            if (player3Position == 31)
            {
                labelPlayer3Piece.Top = pictureBoxRegent.Top + 33;
                labelPlayer3Piece.Left = pictureBoxRegent.Left + 5;
            }
            if (player3Position == 32)
            {
                labelPlayer3Piece.Top = pictureBoxOxford.Top + 33;
                labelPlayer3Piece.Left = pictureBoxOxford.Left + 5;
            }
            if (player3Position == 33)
            {
                labelPlayer3Piece.Top = pictureBoxChest3.Top + 33;
                labelPlayer3Piece.Left = pictureBoxChest3.Left + 5;
                currentSquarePlayer3();
                communityChestPlayer3();
            }
            if (player3Position == 34)
            {
                labelPlayer3Piece.Top = pictureBoxBond.Top + 33;
                labelPlayer3Piece.Left = pictureBoxBond.Left + 5;
            }
            if (player3Position == 35)
            {
                labelPlayer3Piece.Top = pictureBoxTrain4.Top + 33;
                labelPlayer3Piece.Left = pictureBoxTrain4.Left + 5;
            }
            if (player3Position == 36)
            {
                labelPlayer3Piece.Top = pictureBoxChance3.Top + 33;
                labelPlayer3Piece.Left = pictureBoxChance3.Left + 5;
                currentSquarePlayer3();
                chanceCardPlayer3();
            }
            if (player3Position == 37)
            {
                labelPlayer3Piece.Top = pictureBoxParkLane.Top + 33;
                labelPlayer3Piece.Left = pictureBoxParkLane.Left + 5;
            }
            if (player3Position == 38) // added logic for special squares
            {
                labelPlayer3Piece.Top = pictureBoxSuperTax.Top + 33;
                labelPlayer3Piece.Left = pictureBoxSuperTax.Left + 5;
                currentSquarePlayer3();
                if (player3Money < 100)
                {
                    MessageBox.Show("PLAYER 3 - PAY TAX £100!!", "SUPER TAX");
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PAY SUPER TAX");
                    MessageBox.Show("PLAYER 3 - GAME OVER");
                    player3Money = 0;
                    labelP3Money.Visible = false;
                    labelP3Property.Visible = false;
                    labelPlayer3Piece.Visible = false;
                    labelMoney3.Text = "Game";
                    labelPlayer3Property.Text = "Over";
                    p3GameOver = true;
                    returnPropertiesP3();
                }
                else
                {
                    player3Money -= 100;
                    MessageBox.Show("PLAYER 3 - PAY TAX £100!!", "SUPER TAX");
                    labelP3Money.Text = "£" + player3Money.ToString();
                }
            }
            if (player3Position == 39)
            {
                labelPlayer3Piece.Top = pictureBoxMayfair.Top + 33;
                labelPlayer3Piece.Left = pictureBoxMayfair.Left + 5;
            }
        }

        public void player4Movement()
        {
            if (player4Position == 0)
            {
                labelPlayer4Piece.Top = pictureBoxStart.Top + 65;
                labelPlayer4Piece.Left = pictureBoxStart.Left + 70;
            }
            if (player4Position == 1)
            {
                labelPlayer4Piece.Top = pictureBoxOldKent.Top + 65;
                labelPlayer4Piece.Left = pictureBoxOldKent.Left + 39;
            }
            if (player4Position == 2)
            {
                labelPlayer4Piece.Top = pictureBoxChest1.Top + 65;
                labelPlayer4Piece.Left = pictureBoxChest1.Left + 40;
                currentSquarePlayer4();
                communityChestPlayer4();
            }
            if (player4Position == 3)
            {
                labelPlayer4Piece.Top = pictureBoxWhiteChapel.Top + 65;
                labelPlayer4Piece.Left = pictureBoxWhiteChapel.Left + 40;
            }
            if (player4Position == 4)
            {
                labelPlayer4Piece.Top = pictureBoxIncomeTax.Top + 65;
                labelPlayer4Piece.Left = pictureBoxIncomeTax.Left + 40;
                currentSquarePlayer4();
                if (player4Money < 200)
                {
                    MessageBox.Show("PLAYER 4 - PAY TAX £200!!", "INCOME TAX");
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PAY INCOME TAX");
                    MessageBox.Show("PLAYER 4 - GAME OVER");
                    player4Money = 0;
                    labelP4Money.Visible = false;
                    labelP4Property.Visible = false;
                    labelPlayer4Piece.Visible = false;
                    labelMoney4.Text = "Game";
                    labelPlayer4Property.Text = "Over";
                    p4GameOver = true;
                    returnPropertiesP4();
                }
                else
                {
                    player4Money -= 200;
                    MessageBox.Show("PLAYER 4 - PAY TAX £200!!", "INCOME TAX");
                    labelP4Money.Text = "£" + player4Money.ToString();
                }
            }
            if (player4Position == 5)
            {
                labelPlayer4Piece.Top = pictureBoxTrain1.Top + 65;
                labelPlayer4Piece.Left = pictureBoxTrain1.Left + 40;
            }
            if (player4Position == 6)
            {
                labelPlayer4Piece.Top = pictureBoxTheAngel.Top + 65;
                labelPlayer4Piece.Left = pictureBoxTheAngel.Left + 40;
            }
            if (player4Position == 7)
            {
                labelPlayer4Piece.Top = pictureBoxChance1.Top + 65;
                labelPlayer4Piece.Left = pictureBoxChance1.Left + 40;
                currentSquarePlayer4();
                chanceCardPlayer4();
            }
            if (player4Position == 8)
            {
                labelPlayer4Piece.Top = pictureBoxEuston.Top + 65;
                labelPlayer4Piece.Left = pictureBoxEuston.Left + 40;
            }
            if (player4Position == 9)
            {
                labelPlayer4Piece.Top = pictureBoxPentonville.Top + 65;
                labelPlayer4Piece.Left = pictureBoxPentonville.Left + 40;
            }
            if ((player4Position == 10) && (!player4Jail))
            {
                labelPlayer4Piece.Top = pictureBoxJail.Top + 69;
                labelPlayer4Piece.Left = pictureBoxJail.Left + 70;
            }
            if (player4Position == 11)
            {
                labelPlayer4Piece.Top = pictureBoxPallMall.Top + 33;
                labelPlayer4Piece.Left = pictureBoxPallMall.Left + 70;
            }
            if (player4Position == 12)
            {
                labelPlayer4Piece.Top = pictureBoxElectric.Top + 33;
                labelPlayer4Piece.Left = pictureBoxElectric.Left + 70;
            }
            if (player4Position == 13)
            {
                labelPlayer4Piece.Top = pictureBoxWhitehall.Top + 33;
                labelPlayer4Piece.Left = pictureBoxWhitehall.Left + 70;
            }
            if (player4Position == 14)
            {
                labelPlayer4Piece.Top = pictureBoxNorthumrl.Top + 33;
                labelPlayer4Piece.Left = pictureBoxNorthumrl.Left + 70;
            }
            if (player4Position == 15)
            {
                labelPlayer4Piece.Top = pictureBoxTrain2.Top + 33;
                labelPlayer4Piece.Left = pictureBoxTrain2.Left + 70;
            }
            if (player4Position == 16)
            {
                labelPlayer4Piece.Top = pictureBoxBow.Top + 33;
                labelPlayer4Piece.Left = pictureBoxBow.Left + 70;
            }
            if (player4Position == 17)
            {
                labelPlayer4Piece.Top = pictureBoxChest2.Top + 33;
                labelPlayer4Piece.Left = pictureBoxChest2.Left + 70;
                currentSquarePlayer4();
                communityChestPlayer4();
            }
            if (player4Position == 18)
            {
                labelPlayer4Piece.Top = pictureBoxMarlborough.Top + 33;
                labelPlayer4Piece.Left = pictureBoxMarlborough.Left + 70;
            }
            if (player4Position == 19)
            {
                labelPlayer4Piece.Top = pictureBoxVine.Top + 33;
                labelPlayer4Piece.Left = pictureBoxVine.Left + 70;
            }
            if (player4Position == 20)
            {
                labelPlayer4Piece.Top = pictureBoxFreeParking.Top + 65;
                labelPlayer4Piece.Left = pictureBoxFreeParking.Left + 70;
            }
            if (player4Position == 21)
            {
                labelPlayer4Piece.Top = pictureBoxStrand.Top + 65;
                labelPlayer4Piece.Left = pictureBoxStrand.Left + 40;
            }
            if (player4Position == 22)
            {
                labelPlayer4Piece.Top = pictureBoxChance2.Top + 65;
                labelPlayer4Piece.Left = pictureBoxChance2.Left + 40;
                currentSquarePlayer4();
                chanceCardPlayer4();
            }
            if (player4Position == 23)
            {
                labelPlayer4Piece.Top = pictureBoxFleet.Top + 65;
                labelPlayer4Piece.Left = pictureBoxFleet.Left + 40;
            }
            if (player4Position == 24)
            {
                labelPlayer4Piece.Top = pictureBoxTrafalgar.Top + 65;
                labelPlayer4Piece.Left = pictureBoxTrafalgar.Left + 40;
            }
            if (player4Position == 25)
            {
                labelPlayer4Piece.Top = pictureBoxTrain3.Top + 65;
                labelPlayer4Piece.Left = pictureBoxTrain3.Left + 40;
            }
            if (player4Position == 26)
            {
                labelPlayer4Piece.Top = pictureBoxLeicester.Top + 65;
                labelPlayer4Piece.Left = pictureBoxLeicester.Left + 40;
            }
            if (player4Position == 27)
            {
                labelPlayer4Piece.Top = pictureBoxCoventry.Top + 65;
                labelPlayer4Piece.Left = pictureBoxCoventry.Left + 40;
            }
            if (player4Position == 28)
            {
                labelPlayer4Piece.Top = pictureBoxWaterWorks.Top + 65;
                labelPlayer4Piece.Left = pictureBoxWaterWorks.Left + 40;
            }
            if (player4Position == 29)
            {
                labelPlayer4Piece.Top = pictureBoxPicadilly.Top + 65;
                labelPlayer4Piece.Left = pictureBoxPicadilly.Left + 40;
            }
            if ((player4Position == 30) && (!chance4Jail))
            {
                labelPlayer4Piece.Top = pictureBoxGoToJail.Top + 65;
                labelPlayer4Piece.Left = pictureBoxGoToJail.Left + 70;
            }
            if (player4Position == 31)
            {
                labelPlayer4Piece.Top = pictureBoxRegent.Top + 33;
                labelPlayer4Piece.Left = pictureBoxRegent.Left + 70;
            }
            if (player4Position == 32)
            {
                labelPlayer4Piece.Top = pictureBoxOxford.Top + 33;
                labelPlayer4Piece.Left = pictureBoxOxford.Left + 70;
            }
            if (player4Position == 33)
            {
                labelPlayer4Piece.Top = pictureBoxChest3.Top + 33;
                labelPlayer4Piece.Left = pictureBoxChest3.Left + 70;
                currentSquarePlayer4();
                communityChestPlayer4();
            }
            if (player4Position == 34)
            {
                labelPlayer4Piece.Top = pictureBoxBond.Top + 33;
                labelPlayer4Piece.Left = pictureBoxBond.Left + 70;
            }
            if (player4Position == 35)
            {
                labelPlayer4Piece.Top = pictureBoxTrain4.Top + 33;
                labelPlayer4Piece.Left = pictureBoxTrain4.Left + 70;
            }
            if (player4Position == 36)
            {
                labelPlayer4Piece.Top = pictureBoxChance3.Top + 33;
                labelPlayer4Piece.Left = pictureBoxChance3.Left + 70;
                currentSquarePlayer4();
                chanceCardPlayer4();
            }
            if (player4Position == 37)
            {
                labelPlayer4Piece.Top = pictureBoxParkLane.Top + 33;
                labelPlayer4Piece.Left = pictureBoxParkLane.Left + 70;
            }
            if (player4Position == 38) // added logic for special squares
            {
                labelPlayer4Piece.Top = pictureBoxSuperTax.Top + 33;
                labelPlayer4Piece.Left = pictureBoxSuperTax.Left + 70;
                currentSquarePlayer4();
                if (player4Money < 100)
                {
                    MessageBox.Show("PLAYER 4 - PAY TAX £100!!", "SUPER TAX");
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PAY SUPER TAX");
                    MessageBox.Show("PLAYER 4 - GAME OVER");
                    player4Money = 0;
                    labelP4Money.Visible = false;
                    labelP4Property.Visible = false;
                    labelPlayer4Piece.Visible = false;
                    labelMoney4.Text = "Game";
                    labelPlayer4Property.Text = "Over";
                    p4GameOver = true;
                    returnPropertiesP4();
                }
                else
                {
                    player4Money -= 100;
                    MessageBox.Show("PLAYER 4 - PAY TAX £100!!", "SUPER TAX");
                    labelP4Money.Text = "£" + player4Money.ToString();
                }
            }
            if (player4Position == 39)
            {
                labelPlayer4Piece.Top = pictureBoxMayfair.Top + 33;
                labelPlayer4Piece.Left = pictureBoxMayfair.Left + 70;
            }
        }

        // generates random number each time for chance card
        public void chanceCardPlayer1()
        {
            Random ranChanceP1 = new Random();
            int card1 = ranChanceP1.Next(1, 11);
            if (card1 == 1)
            {
                player1Money += 150;
                MessageBox.Show("PLAYER 1 - Your building loan matures. Receive £150", "CHANCE CARD");
                labelP1Money.Text = "£" + player1Money.ToString();
            }
            if (card1 == 2)
            {
                player1Position = 30;
                chance1Jail = true;
                MessageBox.Show("PLAYER 1 - Go to jail. Move directly to jail. Do not pass 'Go'. Do not collect £200!", "CHANCE CARD");
                labelPlayer1Piece.Top = pictureBoxJail.Top + 3;
                labelPlayer1Piece.Left = pictureBoxJail.Left + 35;
            }
            if (card1 == 3)
            {
                player1Money += 100;
                MessageBox.Show("PLAYER 1 - You have won a crossword competition. Collect £100!", "CHANCE CARD");
                labelP1Money.Text = "£" + player1Money.ToString();
            }
            if (card1 == 4)
            {
                player1Position = 0;
                player1Money += 200;
                MessageBox.Show("PLAYER 1 - Go to START square and COLLECT £200!", "CHANCE CARD");
                labelP1Money.Text = "£" + player1Money.ToString();
                player1Movement();
            }
            if (card1 == 5)
            {
                if (player1Money < 150)
                {
                    MessageBox.Show("PLAYER 1 - Pay school fees of £150!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PAY SCHOOL FEES");
                    MessageBox.Show("PLAYER 1 - GAME OVER");
                    player1Money = 0;
                    labelP1Property.Visible = false;
                    labelP1Money.Visible = false;
                    labelPlayer1Piece.Visible = false;
                    labelMoney1.Text = "Game";
                    labelPlayer1Property.Text = "Over";
                    p1GameOver = true;
                    returnPropertiesP1();
                }
                else
                {
                    player1Money -= 150;
                    MessageBox.Show("PLAYER 1 - Pay school fees of £150!", "CHANCE CARD");
                    labelP1Money.Text = "£" + player1Money.ToString();
                }
            }
            if (card1 == 6)
            {
                if (player1Money < 20)
                {
                    MessageBox.Show("PLAYER 1 - 'Drunk in charge' fine £20!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PAY FINE");
                    MessageBox.Show("PLAYER 1 - GAME OVER");
                    player1Money = 0;
                    labelP1Money.Visible = false;
                    labelP1Property.Visible = false;
                    labelPlayer1Piece.Visible = false;
                    labelMoney1.Text = "Game";
                    labelPlayer1Property.Text = "Over";
                    p1GameOver = true;
                    returnPropertiesP1();
                }
                else
                {
                    player1Money -= 20;
                    MessageBox.Show("PLAYER 1 - 'Drunk in charge' fine £20!", "CHANCE CARD");
                    labelP1Money.Text = "£" + player1Money.ToString();
                }
            }
            if (card1 == 7)
            {
                if (player1Money < 15)
                {
                    MessageBox.Show("PLAYER 1 - Speeding fine £15!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PAY FINE");
                    MessageBox.Show("PLAYER 1 - GAME OVER");
                    player1Money = 0;
                    labelP1Money.Visible = false;
                    labelP1Property.Visible = false;
                    labelPlayer1Piece.Visible = false;
                    labelMoney1.Text = "Game";
                    labelPlayer1Property.Text = "Over";
                    p1GameOver = true;
                    returnPropertiesP1();
                }
                else
                {
                    player1Money -= 15;
                    MessageBox.Show("PLAYER 1 - Speeding fine £15!", "CHANCE CARD");
                    labelP1Money.Text = "£" + player1Money.ToString();
                }
            }
            if (card1 == 8)
            {
                player1Money += 100;
                MessageBox.Show("PLAYER 1 - You have won a crossword competition. Collect £100!", "CHANCE CARD");
                labelP1Money.Text = "£" + player1Money.ToString();
            }
            if (card1 == 9)
            {
                player1Money += 50;
                MessageBox.Show("PLAYER 1 - Bank pays you dividend of £50!", "CHANCE CARD");
                labelP1Money.Text = "£" + player1Money.ToString();
            }
            if (card1 == 10)
            {
                player1Position -= 3;
                MessageBox.Show("PLAYER 1 - Go back three spaces!", "CHANCE CARD");
                player1Movement();
            }
        }

        public void chanceCardPlayer2()
        {
            Random ranChanceP2 = new Random();
            int card2 = ranChanceP2.Next(1, 11);
            if (card2 == 1)
            {
                player2Money += 150;
                MessageBox.Show("PLAYER 2 - Your building loan matures. Receive £150", "CHANCE CARD");
                labelP2Money.Text = "£" + player2Money.ToString();
            }
            if (card2 == 2)
            {
                player2Position = 30;
                chance2Jail = true;
                MessageBox.Show("PLAYER 2 - Go to jail. Move directly to jail. Do not pass 'Go'. Do not collect £200!", "CHANCE CARD");
                labelPlayer2Piece.Top = pictureBoxJail.Top + 3;
                labelPlayer2Piece.Left = pictureBoxJail.Left + 72;
            }
            if (card2 == 3)
            {
                player2Money += 100;
                MessageBox.Show("PLAYER 2 - You have won a crossword competition. Collect £100!", "CHANCE CARD");
                labelP2Money.Text = "£" + player2Money.ToString();
            }
            if (card2 == 4)
            {
                player2Position = 0;
                player2Money += 200;
                MessageBox.Show("PLAYER 2 - Go to START square and COLLECT £200!", "CHANCE CARD");
                labelP2Money.Text = "£" + player2Money.ToString();
                player2Movement();
            }
            if (card2 == 5)
            {
                if (player2Money < 150)
                {
                    MessageBox.Show("PLAYER 2 - Pay school fees of £150!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PAY SCHOOL FEES");
                    MessageBox.Show("PLAYER 2 - GAME OVER");
                    player2Money = 0;
                    labelP2Money.Visible = false;
                    labelP2Property.Visible = false;
                    labelPlayer2Piece.Visible = false;
                    labelMoney2.Text = "Game";
                    labelPlayer2Property.Text = "Over";
                    p2GameOver = true;
                    returnPropertiesP2();
                }
                else
                {
                    player2Money -= 150;
                    MessageBox.Show("PLAYER 2 - Pay school fees of £150!", "CHANCE CARD");
                    labelP2Money.Text = "£" + player2Money.ToString();
                }
            }
            if (card2 == 6)
            {
                if (player2Money < 20)
                {
                    MessageBox.Show("PLAYER 2 - 'Drunk in charge' fine £20!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PAY FINE");
                    MessageBox.Show("PLAYER 2 - GAME OVER");
                    player2Money = 0;
                    labelP2Money.Visible = false;
                    labelP2Property.Visible = false;
                    labelPlayer2Piece.Visible = false;
                    labelMoney2.Text = "Game";
                    labelPlayer2Property.Text = "Over";
                    p2GameOver = true;
                    returnPropertiesP2();
                }
                else
                {
                    player2Money -= 20;
                    MessageBox.Show("PLAYER 2 - 'Drunk in charge' fine £20!", "CHANCE CARD");
                    labelP2Money.Text = "£" + player2Money.ToString();
                }
            }
            if (card2 == 7)
            {
                if (player2Money < 15)
                {
                    MessageBox.Show("PLAYER 2 - Speeding fine £15!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PAY FINE");
                    MessageBox.Show("PLAYER 2 - GAME OVER");
                    player2Money = 0;
                    labelP2Money.Visible = false;
                    labelP2Property.Visible = false;
                    labelPlayer2Piece.Visible = false;
                    labelMoney2.Text = "Game";
                    labelPlayer2Property.Text = "Over";
                    p2GameOver = true;
                    returnPropertiesP2();
                }
                else
                {
                    player2Money -= 15;
                    MessageBox.Show("PLAYER 2 - Speeding fine £15!", "CHANCE CARD");
                    labelP2Money.Text = "£" + player2Money.ToString();
                }
            }
            if (card2 == 8)
            {
                player2Money += 100;
                MessageBox.Show("PLAYER 2 - You have won a crossword competition. Collect £100!", "CHANCE CARD");
                labelP2Money.Text = "£" + player2Money.ToString();
            }
            if (card2 == 9)
            {
                player2Money += 50;
                MessageBox.Show("PLAYER 2 - Bank pays you dividend of £50!", "CHANCE CARD");
                labelP2Money.Text = "£" + player2Money.ToString();
            }
            if (card2 == 10)
            {
                player2Position -= 3;
                MessageBox.Show("PLAYER 2 - Go back three spaces!", "CHANCE CARD");
                player2Movement();
            }
        }

        public void chanceCardPlayer3()
        {
            Random ranChanceP3 = new Random();
            int card3 = ranChanceP3.Next(1, 11);
            if (card3 == 1)
            {
                player3Money += 150;
                MessageBox.Show("PLAYER 3 - Your building loan matures. Receive £150", "CHANCE CARD");
                labelP3Money.Text = "£" + player3Money.ToString();
            }
            if (card3 == 2)
            {
                player3Position = 30;
                chance3Jail = true;
                MessageBox.Show("PLAYER 3 - Go to jail. Move directly to jail. Do not pass 'Go'. Do not collect £200!", "CHANCE CARD");
                labelPlayer3Piece.Top = pictureBoxJail.Top + 40;
                labelPlayer3Piece.Left = pictureBoxJail.Left + 35;
            }
            if (card3 == 3)
            {
                player3Money += 100;
                MessageBox.Show("PLAYER 3 - You have won a crossword competition. Collect £100!", "CHANCE CARD");
                labelP3Money.Text = "£" + player3Money.ToString();
            }
            if (card3 == 4)
            {
                player3Position = 0;
                player3Money += 200;
                MessageBox.Show("PLAYER 3 - Go to START square and COLLECT £200!", "CHANCE CARD");
                labelP3Money.Text = "£" + player3Money.ToString();
                player3Movement();
            }
            if (card3 == 5)
            {
                if (player3Money < 150)
                {
                    MessageBox.Show("PLAYER 3 - Pay school fees of £150!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PAY SCHOOL FEES");
                    MessageBox.Show("PLAYER 3 - GAME OVER");
                    player3Money = 0;
                    labelP3Money.Visible = false;
                    labelP3Property.Visible = false;
                    labelPlayer3Piece.Visible = false;
                    labelMoney3.Text = "Game";
                    labelPlayer3Property.Text = "Over";
                    p3GameOver = true;
                    returnPropertiesP3();
                }
                else
                {
                    player3Money -= 150;
                    MessageBox.Show("PLAYER 3 - Pay school fees of £150!", "CHANCE CARD");
                    labelP3Money.Text = "£" + player3Money.ToString();
                }
            }
            if (card3 == 6)
            {
                if (player3Money < 20)
                {
                    MessageBox.Show("PLAYER 3 - 'Drunk in charge' fine £20!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PAY FINE");
                    MessageBox.Show("PLAYER 3 - GAME OVER");
                    player3Money = 0;
                    labelP3Money.Visible = false;
                    labelP3Property.Visible = false;
                    labelPlayer3Piece.Visible = false;
                    labelMoney3.Text = "Game";
                    labelPlayer3Property.Text = "Over";
                    p3GameOver = true;
                    returnPropertiesP3();
                }
                else
                {
                    player3Money -= 20;
                    MessageBox.Show("PLAYER 3 - 'Drunk in charge' fine £20!", "CHANCE CARD");
                    labelP3Money.Text = "£" + player3Money.ToString();
                }
            }
            if (card3 == 7)
            {
                if (player3Money < 15)
                {
                    MessageBox.Show("PLAYER 3 - Speeding fine £15!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PAY FINE");
                    MessageBox.Show("PLAYER 3 - GAME OVER");
                    player3Money = 0;
                    labelP3Money.Visible = false;
                    labelP3Property.Visible = false;
                    labelPlayer3Piece.Visible = false;
                    labelMoney3.Text = "Game";
                    labelPlayer3Property.Text = "Over";
                    p3GameOver = true;
                    returnPropertiesP3();
                }
                else
                {
                    player3Money -= 15;
                    MessageBox.Show("PLAYER 3 - Speeding fine £15!", "CHANCE CARD");
                    labelP3Money.Text = "£" + player3Money.ToString();
                }
            }
            if (card3 == 8)
            {
                player3Money += 100;
                MessageBox.Show("PLAYER 3 - You have won a crossword competition. Collect £100!", "CHANCE CARD");
                labelP3Money.Text = "£" + player3Money.ToString();
            }
            if (card3 == 9)
            {
                player3Money += 50;
                MessageBox.Show("PLAYER 3 - Bank pays you dividend of £50!", "CHANCE CARD");
                labelP3Money.Text = "£" + player3Money.ToString();
            }
            if (card3 == 10)
            {
                player3Position -= 3;
                MessageBox.Show("PLAYER 3 - Go back three spaces!", "CHANCE CARD");
                player3Movement();
            }
        }

        public void chanceCardPlayer4()
        {
            Random ranChanceP4 = new Random();
            int card4 = ranChanceP4.Next(1, 11);
            if (card4 == 1)
            {
                player4Money += 150;
                MessageBox.Show("PLAYER 4 - Your building loan matures. Receive £150", "CHANCE CARD");
                labelP4Money.Text = "£" + player4Money.ToString();
            }
            if (card4 == 2)
            {
                player4Position = 30;
                chance4Jail = true;
                MessageBox.Show("PLAYER 4 - Go to jail. Move directly to jail. Do not pass 'Go'. Do not collect £200!", "CHANCE CARD");
                labelPlayer4Piece.Top = pictureBoxJail.Top + 40;
                labelPlayer4Piece.Left = pictureBoxJail.Left + 72;
            }
            if (card4 == 3)
            {
                player4Money += 100;
                MessageBox.Show("PLAYER 4 - You have won a crossword competition. Collect £100!", "CHANCE CARD");
                labelP4Money.Text = "£" + player4Money.ToString();
            }
            if (card4 == 4)
            {
                player4Position = 0;
                player4Money += 200;
                MessageBox.Show("PLAYER 4 - Go to START square and COLLECT £200!", "CHANCE CARD");
                labelP2Money.Text = "£" + player2Money.ToString();
                player4Movement();
            }
            if (card4 == 5)
            {
                if (player4Money < 150)
                {
                    MessageBox.Show("PLAYER 4 - Pay school fees of £150!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PAY SCHOOL FEES");
                    MessageBox.Show("PLAYER 4 - GAME OVER");
                    player4Money = 0;
                    labelP4Money.Visible = false;
                    labelP4Property.Visible = false;
                    labelPlayer4Piece.Visible = false;
                    labelMoney4.Text = "Game";
                    labelPlayer4Property.Text = "Over";
                    p4GameOver = true;
                    returnPropertiesP4();
                }
                else
                {
                    player4Money -= 150;
                    MessageBox.Show("PLAYER 4 - Pay school fees of £150!", "CHANCE CARD");
                    labelP4Money.Text = "£" + player4Money.ToString();
                }
            }
            if (card4 == 6)
            {
                if (player4Money < 20)
                {
                    MessageBox.Show("PLAYER 4 - 'Drunk in charge' fine £20!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PAY FINE");
                    MessageBox.Show("PLAYER 4 - GAME OVER");
                    player4Money = 0;
                    labelP4Money.Visible = false;
                    labelP4Property.Visible = false;
                    labelPlayer4Piece.Visible = false;
                    labelMoney4.Text = "Game";
                    labelPlayer4Property.Text = "Over";
                    p4GameOver = true;
                    returnPropertiesP4();
                }
                else
                {
                    player4Money -= 20;
                    MessageBox.Show("PLAYER 4 - 'Drunk in charge' fine £20!", "CHANCE CARD");
                    labelP4Money.Text = "£" + player4Money.ToString();
                }
            }
            if (card4 == 7)
            {
                if (player4Money < 15)
                {
                    MessageBox.Show("PLAYER 4 - Speeding fine £15!", "CHANCE CARD");
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PAY FINE");
                    MessageBox.Show("PLAYER 4 - GAME OVER");
                    player4Money = 0;
                    labelP4Money.Visible = false;
                    labelP4Property.Visible = false;
                    labelPlayer4Piece.Visible = false;
                    labelMoney4.Text = "Game";
                    labelPlayer4Property.Text = "Over";
                    p4GameOver = true;
                    returnPropertiesP4();
                }
                else
                {
                    player4Money -= 15;
                    MessageBox.Show("PLAYER 4 - Speeding fine £15!", "CHANCE CARD");
                    labelP4Money.Text = "£" + player4Money.ToString();
                }
            }
            if (card4 == 8)
            {
                player4Money += 100;
                MessageBox.Show("PLAYER 4 - You have won a crossword competition. Collect £100!", "CHANCE CARD");
                labelP4Money.Text = "£" + player4Money.ToString();
            }
            if (card4 == 9)
            {
                player4Money += 50;
                MessageBox.Show("PLAYER 4 - Bank pays you dividend of £50!", "CHANCE CARD");
                labelP4Money.Text = "£" + player4Money.ToString();
            }
            if (card4 == 10)
            {
                player4Position -= 3;
                MessageBox.Show("PLAYER 4 - Go back three spaces!", "CHANCE CARD");
                player4Movement();
            }
        }

        // generates random number when community chest card is used
        public void communityChestPlayer1()
        {
            Random ranChestP1 = new Random();
            int chest1 = ranChestP1.Next(1, 10);
            if (chest1 == 1)
            {
                player1Position = 1;
                MessageBox.Show("PLAYER 1 - Go back to Old Kent Road!", "COMMUNITY CHEST CARD");
                player1Movement();
            }
            if (chest1 == 2)
            {
                if (player1Money < 50)
                {
                    MessageBox.Show("PLAYER 1 - Pay your insurance premium £50!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PAY INSURANCE");
                    MessageBox.Show("PLAYER 1 - GAME OVER");
                    player1Money = 0;
                    labelP1Money.Visible = false;
                    labelP1Property.Visible = false;
                    labelPlayer1Piece.Visible = false;
                    labelMoney1.Text = "Game";
                    labelPlayer1Property.Text = "Over";
                    p1GameOver = true;
                    returnPropertiesP1();
                }
                else
                {
                    player1Money -= 50;
                    MessageBox.Show("PLAYER 1 - Pay your insurance premium £50!", "COMMUNITY CHEST CARD");
                    labelP1Money.Text = "£" + player1Money.ToString();
                }
            }
            if (chest1 == 3)
            {
                if (player1Money < 50)
                {
                    MessageBox.Show("PLAYER 1 - Doctor's fee. Pay £50!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PAY DOCTORS FEE");
                    MessageBox.Show("PLAYER 1 - GAME OVER");
                    player1Money = 0;
                    labelP1Money.Visible = false;
                    labelP1Property.Visible = false;
                    labelPlayer1Piece.Visible = false;
                    labelMoney1.Text = "Game";
                    labelPlayer1Property.Text = "Over";
                    p1GameOver = true;
                    returnPropertiesP1();
                }
                else
                {
                    player1Money -= 50;
                    MessageBox.Show("PLAYER 1 - Doctor's fee. Pay £50!", "COMMUNITY CHEST CARD");
                    labelP1Money.Text = "£" + player1Money.ToString();
                }
            }
            if (chest1 == 4)
            {
                player1Money += 20;
                MessageBox.Show("PLAYER 1 - Income tax refund. Collect £20!", "COMMUNITY CHEST CARD");
                labelP1Money.Text = "£" + player1Money.ToString();
            }
            if (chest1 == 5)
            {
                player1Position = 30;
                chance1Jail = true;
                MessageBox.Show("PLAYER 1 - Go to jail. Move directly to jail. Do not pass 'Go'. Do not collect £200!", "COMMUNITY CHEST CARD");
                labelPlayer1Piece.Top = pictureBoxJail.Top + 3;
                labelPlayer1Piece.Left = pictureBoxJail.Left + 35;
            }
            if (chest1 == 6)
            {
                if (player1Money < 100)
                {
                    MessageBox.Show("PLAYER 1 - Pay hospital £100!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PAY HOSPITAL");
                    MessageBox.Show("PLAYER 1 - GAME OVER");
                    player1Money = 0;
                    labelP1Money.Visible = false;
                    labelP1Property.Visible = false;
                    labelPlayer1Piece.Visible = false;
                    labelMoney1.Text = "Game";
                    labelPlayer1Property.Text = "Over";
                    p1GameOver = true;
                    returnPropertiesP1();
                }
                else
                {
                    player1Money -= 100;
                    MessageBox.Show("PLAYER 1 - Pay hospital £100!", "COMMUNITY CHEST CARD");
                    labelP1Money.Text = "£" + player1Money.ToString();
                }
            }
            if (chest1 == 7)
            {
                player1Money += 10;
                MessageBox.Show("PLAYER 1 - You have won second prize in a beauty contest. Collect £10!", "COMMUNITY CHEST CARD");
                labelP1Money.Text = "£" + player1Money.ToString();
            }
            if (chest1 == 8)
            {
                player1Money += 100;
                MessageBox.Show("PLAYER 1 - You inherit £100!", "COMMUNITY CHEST CARD");
                labelP1Money.Text = "£" + player1Money.ToString();
            }
            if (chest1 == 9)
            {
                player1Money += 50;
                MessageBox.Show("PLAYER 1 - From sale of stock you get £50!", "COMMUNITY CHEST CARD");
                labelP1Money.Text = "£" + player1Money.ToString();
            }
        }

        public void communityChestPlayer2()
        {
            Random ranChestP2 = new Random();
            int chest2 = ranChestP2.Next(1, 10);
            if (chest2 == 1)
            {
                player2Position = 1;
                MessageBox.Show("PLAYER 2 - Go back to Old Kent Road!", "COMMUNITY CHEST CARD");
                player2Movement();
            }
            if (chest2 == 2)
            {
                if (player2Money < 50)
                {
                    MessageBox.Show("PLAYER 2 - Pay your insurance premium £50!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PAY INSURANCE");
                    MessageBox.Show("PLAYER 2 - GAME OVER");
                    player2Money = 0;
                    labelP2Money.Visible = false;
                    labelP2Property.Visible = false;
                    labelPlayer2Piece.Visible = false;
                    labelMoney2.Text = "Game";
                    labelPlayer2Property.Text = "Over";
                    p2GameOver = true;
                    returnPropertiesP2();
                }
                else
                {
                    player2Money -= 50;
                    MessageBox.Show("PLAYER 2 - Pay your insurance premium £50!", "COMMUNITY CHEST CARD");
                    labelP2Money.Text = "£" + player2Money.ToString();
                }
            }
            if (chest2 == 3)
            {
                if (player2Money < 50)
                {
                    MessageBox.Show("PLAYER 2 - Doctor's fee. Pay £50!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PAY DOCTORS FEE");
                    MessageBox.Show("PLAYER 2 - GAME OVER");
                    player2Money = 0;
                    labelP2Money.Visible = false;
                    labelP2Property.Visible = false;
                    labelPlayer2Piece.Visible = false;
                    labelMoney2.Text = "Game";
                    labelPlayer2Property.Text = "Over";
                    p2GameOver = true;
                    returnPropertiesP2();
                }
                else
                {
                    player2Money -= 50;
                    MessageBox.Show("PLAYER 2 - Doctor's fee. Pay £50!", "COMMUNITY CHEST CARD");
                    labelP2Money.Text = "£" + player2Money.ToString();
                }
            }
            if (chest2 == 4)
            {
                player2Money += 20;
                MessageBox.Show("PLAYER 2 - Income tax refund. Collect £20!", "COMMUNITY CHEST CARD");
                labelP2Money.Text = "£" + player2Money.ToString();
            }
            if (chest2 == 5)
            {
                player2Position = 30;
                chance2Jail = true;
                MessageBox.Show("PLAYER 2 - Go to jail. Move directly to jail. Do not pass 'Go'. Do not collect £200!", "COMMUNITY CHEST CARD");
                labelPlayer2Piece.Top = pictureBoxJail.Top + 3;
                labelPlayer2Piece.Left = pictureBoxJail.Left + 72;
            }
            if (chest2 == 6)
            {
                if (player2Money < 100)
                {
                    MessageBox.Show("PLAYER 2 - Pay hospital £100!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PAY HOSPITAL");
                    MessageBox.Show("PLAYER 2 - GAME OVER");
                    player2Money = 0;
                    labelP2Money.Visible = false;
                    labelP2Property.Visible = false;
                    labelPlayer2Piece.Visible = false;
                    labelMoney2.Text = "Game";
                    labelPlayer2Property.Text = "Over";
                    p2GameOver = true;
                    returnPropertiesP2();
                }
                else
                {
                    player2Money -= 100;
                    MessageBox.Show("PLAYER 2 - Pay hospital £100!", "COMMUNITY CHEST CARD");
                    labelP2Money.Text = "£" + player2Money.ToString();
                }
            }
            if (chest2 == 7)
            {
                player2Money += 10;
                MessageBox.Show("PLAYER 2 - You have won second prize in a beauty contest. Collect £10!", "COMMUNITY CHEST CARD");
                labelP2Money.Text = "£" + player2Money.ToString();
            }
            if (chest2 == 8)
            {
                player2Money += 100;
                MessageBox.Show("PLAYER 2 - You inherit £100!", "COMMUNITY CHEST CARD");
                labelP2Money.Text = "£" + player2Money.ToString();
            }
            if (chest2 == 9)
            {
                player2Money += 50;
                MessageBox.Show("PLAYER 2 - From sale of stock you get £50!", "COMMUNITY CHEST CARD");
                labelP2Money.Text = "£" + player2Money.ToString();
            }
        }

        public void communityChestPlayer3()
        {
            Random ranChestP3 = new Random();
            int chest3 = ranChestP3.Next(1, 10);
            if (chest3 == 1)
            {
                player3Position = 1;
                MessageBox.Show("PLAYER 3 - Go back to Old Kent Road!", "COMMUNITY CHEST CARD");
                player3Movement();
            }
            if (chest3 == 2)
            {
                if (player3Money < 50)
                {
                    MessageBox.Show("PLAYER 3 - Pay your insurance premium £50!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PAY INSURANCE");
                    MessageBox.Show("PLAYER 3 - GAME OVER");
                    player3Money = 0;
                    labelP3Money.Visible = false;
                    labelP3Property.Visible = false;
                    labelPlayer3Piece.Visible = false;
                    labelMoney3.Text = "Game";
                    labelPlayer3Property.Text = "Over";
                    p3GameOver = true;
                    returnPropertiesP3();
                }
                else
                {
                    player3Money -= 50;
                    MessageBox.Show("PLAYER 3 - Pay your insurance premium £50!", "COMMUNITY CHEST CARD");
                    labelP3Money.Text = "£" + player3Money.ToString();
                }
            }
            if (chest3 == 3)
            {
                if (player3Money < 50)
                {
                    MessageBox.Show("PLAYER 3 - Doctor's fee. Pay £50!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PAY DOCTORS FEE");
                    MessageBox.Show("PLAYER 3 - GAME OVER");
                    player3Money = 0;
                    labelP3Money.Visible = false;
                    labelP3Property.Visible = false;
                    labelPlayer3Piece.Visible = false;
                    labelMoney3.Text = "Game";
                    labelPlayer3Property.Text = "Over";
                    p3GameOver = true;
                    returnPropertiesP3();
                }
                else
                {
                    player3Money -= 50;
                    MessageBox.Show("PLAYER 3 - Doctor's fee. Pay £50!", "COMMUNITY CHEST CARD");
                    labelP3Money.Text = "£" + player3Money.ToString();
                }
            }
            if (chest3 == 4)
            {
                player3Money += 20;
                MessageBox.Show("PLAYER 3 - Income tax refund. Collect £20!", "COMMUNITY CHEST CARD");
                labelP3Money.Text = "£" + player3Money.ToString();
            }
            if (chest3 == 5)
            {
                player3Position = 30;
                chance3Jail = true;
                MessageBox.Show("PLAYER 3 - Go to jail. Move directly to jail. Do not pass 'Go'. Do not collect £200!", "COMMUNITY CHEST CARD");
                labelPlayer3Piece.Top = pictureBoxJail.Top + 40;
                labelPlayer3Piece.Left = pictureBoxJail.Left + 35;
            }
            if (chest3 == 6)
            {
                if (player3Money < 100)
                {
                    MessageBox.Show("PLAYER 3 - Pay hospital £100!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PAY HOSPITAL");
                    MessageBox.Show("PLAYER 3 - GAME OVER");
                    player3Money = 0;
                    labelP3Money.Visible = false;
                    labelP3Property.Visible = false;
                    labelPlayer3Piece.Visible = false;
                    labelMoney3.Text = "Game";
                    labelPlayer3Property.Text = "Over";
                    p3GameOver = true;
                    returnPropertiesP3();
                }
                else
                {
                    player3Money -= 100;
                    MessageBox.Show("PLAYER 3 - Pay hospital £100!", "COMMUNITY CHEST CARD");
                    labelP3Money.Text = "£" + player3Money.ToString();
                }
            }
            if (chest3 == 7)
            {
                player3Money += 10;
                MessageBox.Show("PLAYER 3 - You have won second prize in a beauty contest. Collect £10!", "COMMUNITY CHEST CARD");
                labelP3Money.Text = "£" + player3Money.ToString();
            }
            if (chest3 == 8)
            {
                player3Money += 100;
                MessageBox.Show("PLAYER 3 - You inherit £100!", "COMMUNITY CHEST CARD");
                labelP3Money.Text = "£" + player3Money.ToString();
            }
            if (chest3 == 9)
            {
                player3Money += 50;
                MessageBox.Show("PLAYER 3 - From sale of stock you get £50!", "COMMUNITY CHEST CARD");
                labelP3Money.Text = "£" + player3Money.ToString();
            }
        }

        public void communityChestPlayer4()
        {
            Random ranChestP4 = new Random();
            int chest4 = ranChestP4.Next(1, 10);
            if (chest4 == 1)
            {
                player4Position = 1;
                MessageBox.Show("PLAYER 4 - Go back to Old Kent Road!", "COMMUNITY CHEST CARD");
                player4Movement();
            }
            if (chest4 == 2)
            {
                if (player4Money < 50)
                {
                    MessageBox.Show("PLAYER 4 - Pay your insurance premium £50!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PAY INSURANCE");
                    MessageBox.Show("PLAYER 4 - GAME OVER");
                    player4Money = 0;
                    labelP4Money.Visible = false;
                    labelP4Property.Visible = false;
                    labelPlayer4Piece.Visible = false;
                    labelMoney4.Text = "Game";
                    labelPlayer4Property.Text = "Over";
                    p4GameOver = true;
                    returnPropertiesP4();
                }
                else
                {
                    player4Money -= 50;
                    MessageBox.Show("PLAYER 4 - Pay your insurance premium £50!", "COMMUNITY CHEST CARD");
                    labelP4Money.Text = "£" + player4Money.ToString();
                }
            }
            if (chest4 == 3)
            {
                if (player4Money < 50)
                {
                    MessageBox.Show("PLAYER 4 - Doctor's fee. Pay £50!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PAY DOCTORS FEE");
                    MessageBox.Show("PLAYER 4 - GAME OVER");
                    player4Money = 0;
                    labelP4Money.Visible = false;
                    labelP4Property.Visible = false;
                    labelPlayer4Piece.Visible = false;
                    labelMoney4.Text = "Game";
                    labelPlayer4Property.Text = "Over";
                    p4GameOver = true;
                    returnPropertiesP4();
                }
                else
                {
                    player4Money -= 50;
                    MessageBox.Show("PLAYER 4 - Doctor's fee. Pay £50!", "COMMUNITY CHEST CARD");
                    labelP4Money.Text = "£" + player4Money.ToString();
                }
            }
            if (chest4 == 4)
            {
                player4Money += 20;
                MessageBox.Show("PLAYER 4 - Income tax refund. Collect £20!", "COMMUNITY CHEST CARD");
                labelP4Money.Text = "£" + player4Money.ToString();
            }
            if (chest4 == 5)
            {
                player4Position = 30;
                chance4Jail = true;
                MessageBox.Show("PLAYER 4 - Go to jail. Move directly to jail. Do not pass 'Go'. Do not collect £200!", "COMMUNITY CHEST CARD");
                labelPlayer4Piece.Top = pictureBoxJail.Top + 40;
                labelPlayer4Piece.Left = pictureBoxJail.Left + 72;
            }
            if (chest4 == 6)
            {
                if (player4Money < 100)
                {
                    MessageBox.Show("PLAYER 4 - Pay hospital £100!", "COMMUNITY CHEST CARD");
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PAY HOSPITAL");
                    MessageBox.Show("PLAYER 4 - GAME OVER");
                    player4Money = 0;
                    labelP4Money.Visible = false;
                    labelP4Property.Visible = false;
                    labelPlayer4Piece.Visible = false;
                    labelMoney4.Text = "Game";
                    labelPlayer4Property.Text = "Over";
                    p4GameOver = true;
                    returnPropertiesP4();
                }
                else
                {
                    player4Money -= 100;
                    MessageBox.Show("PLAYER 4 - Pay hospital £100!", "COMMUNITY CHEST CARD");
                    labelP4Money.Text = "£" + player4Money.ToString();
                }
            }
            if (chest4 == 7)
            {
                player4Money += 10;
                MessageBox.Show("PLAYER 4 - You have won second prize in a beauty contest. Collect £10!", "COMMUNITY CHEST CARD");
                labelP4Money.Text = "£" + player4Money.ToString();
            }
            if (chest4 == 8)
            {
                player4Money += 100;
                MessageBox.Show("PLAYER 4 - You inherit £100!", "COMMUNITY CHEST CARD");
                labelP4Money.Text = "£" + player4Money.ToString();
            }
            if (chest4 == 9)
            {
                player4Money += 50;
                MessageBox.Show("PLAYER 4 - From sale of stock you get £50!", "COMMUNITY CHEST CARD");
                labelP4Money.Text = "£" + player4Money.ToString();
            }
        }

        // checks status when player is on specific square - if it is owned by someone or not if he can buy it maybe
        public void currentSquarePlayer1()
        {
            if ((player1Position == 0) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position0.png");
                labelPropertyCost.Text = "COLLECT: £200";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "GO SQUARE";
            }
            if ((player1Position == 1) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position1.png");
                labelPropertyCost.Text = "COST: £60";
                labelPropertyRent.Text = "RENT :£2";
                groupBoxStart.Text = "OLD KENT ROAD";
            }
            if ((player1Position == 2) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position2.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player1Position == 3) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position3.png");
                labelPropertyCost.Text = "COST: £60";
                labelPropertyRent.Text = "RENT :£4";
                groupBoxStart.Text = "WHITE CHAPEL ROAD";
            }
            if ((player1Position == 4) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position4.png");
                labelPropertyCost.Text = "PAY: £200";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "INCOME TAX";
            }
            if ((player1Position == 5) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position5.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "KING CROSS STATION";
            }
            if ((player1Position == 6) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position6.png");
                labelPropertyCost.Text = "COST: £100";
                labelPropertyRent.Text = "RENT :£6";
                groupBoxStart.Text = "THE ANGEL ISLINGTON";
            }
            if ((player1Position == 7) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position7.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player1Position == 8) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position8.png");
                labelPropertyCost.Text = "COST: £100";
                labelPropertyRent.Text = "RENT :£6";
                groupBoxStart.Text = "EUSTON ROAD";
            }
            if ((player1Position == 9) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position9.png");
                labelPropertyCost.Text = "COST: £120";
                labelPropertyRent.Text = "RENT :£8";
                groupBoxStart.Text = "PENTONVILLE ROAD";
            }
            if ((player1Position == 10) && (turn == 1) && (!player1Jail))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position10.png");
                labelPropertyCost.Text = "PLAYER 1 - VISITS JAIL";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "JUST VISITING";
            }
            if ((player1Position == 10) && (turn == 1) && (player1Jail))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position10.png");
                labelPropertyCost.Text = "PLAYER 1 - ARRESTED";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "PRISON";
            }
            if ((player1Position == 11) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position11.png");
                labelPropertyCost.Text = "COST: £140";
                labelPropertyRent.Text = "RENT :£10";
                groupBoxStart.Text = "PALL MALL";
            }
            if ((player1Position == 12) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position12.png");
                labelPropertyCost.Text = "COST: £150";
                labelPropertyRent.Text = "RENT :£15";
                groupBoxStart.Text = "ELECTRIC COMPANY";
            }
            if ((player1Position == 13) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position13.png");
                labelPropertyCost.Text = "COST: £140";
                labelPropertyRent.Text = "RENT :£10";
                groupBoxStart.Text = "WHITEHALL";
            }
            if ((player1Position == 14) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position14.png");
                labelPropertyCost.Text = "COST: £160";
                labelPropertyRent.Text = "RENT :£12";
                groupBoxStart.Text = "NOTRHUMBERLAND AVENUE";
            }
            if ((player1Position == 15) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position15.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "MARYLEBONE STATION";
            }
            if ((player1Position == 16) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position16.png");
                labelPropertyCost.Text = "COST: £180";
                labelPropertyRent.Text = "RENT :£14";
                groupBoxStart.Text = "BOW STREET";
            }
            if ((player1Position == 17) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position17.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player1Position == 18) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position18.png");
                labelPropertyCost.Text = "COST: £180";
                labelPropertyRent.Text = "RENT :£14";
                groupBoxStart.Text = "MARLBOROUGH STREET";
            }
            if ((player1Position == 19) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position19.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£16";
                groupBoxStart.Text = "VINE STREET";
            }
            if ((player1Position == 20) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position20.png");
                labelPropertyCost.Text = "DRINK SOME COFFEE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "FREE PARKING SQUARE";
            }
            if ((player1Position == 21) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position21.png");
                labelPropertyCost.Text = "COST: £220";
                labelPropertyRent.Text = "RENT :£18";
                groupBoxStart.Text = "STRAND";
            }
            if ((player1Position == 22) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position22.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player1Position == 23) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position23.png");
                labelPropertyCost.Text = "COST: £220";
                labelPropertyRent.Text = "RENT :£18";
                groupBoxStart.Text = "FLEET STREET";
            }
            if ((player1Position == 24) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position24.png");
                labelPropertyCost.Text = "COST: £240";
                labelPropertyRent.Text = "RENT :£20";
                groupBoxStart.Text = "TRAFALGAR SQUARE";
            }
            if ((player1Position == 25) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position25.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "FENCHURCH STREET STATION";
            }
            if ((player1Position == 26) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position26.png");
                labelPropertyCost.Text = "COST: £260";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "LEICESTER SQUARE";
            }
            if ((player1Position == 27) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position27.png");
                labelPropertyCost.Text = "COST: £260";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "COVENTRY STREET";
            }
            if ((player1Position == 28) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position28.png");
                labelPropertyCost.Text = "COST: £150";
                labelPropertyRent.Text = "RENT :£15";
                groupBoxStart.Text = "WATER WORKS";
            }
            if ((player1Position == 29) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position29.png");
                labelPropertyCost.Text = "COST: £280";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "PICCADILLY";
            }
            if ((player1Position == 30) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position30.png");
                labelPropertyCost.Text = "GO TO JAIL";
                labelPropertyRent.Text = "AND STAY THERE";
                groupBoxStart.Text = "GO TO JAIL SQUARE";
            }
            if ((player1Position == 31) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position31.png");
                labelPropertyCost.Text = "COST: £300";
                labelPropertyRent.Text = "RENT :£26";
                groupBoxStart.Text = "REGENT STREET";
            }
            if ((player1Position == 32) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position32.png");
                labelPropertyCost.Text = "COST: £300";
                labelPropertyRent.Text = "RENT :£26";
                groupBoxStart.Text = "OXFORD STREET";
            }
            if ((player1Position == 33) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position33.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player1Position == 34) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position34.png");
                labelPropertyCost.Text = "COST: £320";
                labelPropertyRent.Text = "RENT :£28";
                groupBoxStart.Text = "BOND STREET";
            }
            if ((player1Position == 35) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position35.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "LIVERPOOL STREET STATION";
            }
            if ((player1Position == 36) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position36.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player1Position == 37) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position37.png");
                labelPropertyCost.Text = "COST: £350";
                labelPropertyRent.Text = "RENT :£35";
                groupBoxStart.Text = "PARK LANE";
            }
            if ((player1Position == 38) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position38.png");
                labelPropertyCost.Text = "PAY: £100";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "SUPER TAX";
            }
            if ((player1Position == 39) && (turn == 1))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position39.png");
                labelPropertyCost.Text = "COST: £400";
                labelPropertyRent.Text = "RENT :£50";
                groupBoxStart.Text = "MAYFAIR";
            }
            propertyOwnership();
        }

        public void currentSquarePlayer2()
        {
            if ((player2Position == 0) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position0.png");
                labelPropertyCost.Text = "COLLECT: £200";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "GO SQUARE";
            }
            if ((player2Position == 1) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position1.png");
                labelPropertyCost.Text = "COST: £60";
                labelPropertyRent.Text = "RENT :£2";
                groupBoxStart.Text = "OLD KENT ROAD";
            }
            if ((player2Position == 2) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position2.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player2Position == 3) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position3.png");
                labelPropertyCost.Text = "COST: £60";
                labelPropertyRent.Text = "RENT :£4";
                groupBoxStart.Text = "WHITE CHAPEL ROAD";
            }
            if ((player2Position == 4) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position4.png");
                labelPropertyCost.Text = "PAY: £200";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "INCOME TAX";
            }
            if ((player2Position == 5) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position5.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "KING CROSS STATION";
            }
            if ((player2Position == 6) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position6.png");
                labelPropertyCost.Text = "COST: £100";
                labelPropertyRent.Text = "RENT :£6";
                groupBoxStart.Text = "THE ANGEL ISLINGTON";
            }
            if ((player2Position == 7) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position7.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player2Position == 8) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position8.png");
                labelPropertyCost.Text = "COST: £100";
                labelPropertyRent.Text = "RENT :£6";
                groupBoxStart.Text = "EUSTON ROAD";
            }
            if ((player2Position == 9) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position9.png");
                labelPropertyCost.Text = "COST: £120";
                labelPropertyRent.Text = "RENT :£8";
                groupBoxStart.Text = "PENTONVILLE ROAD";
            }
            if ((player2Position == 10) && (turn == 2) && (!player2Jail))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position10.png");
                labelPropertyCost.Text = "PLAYER 2 - VISITS JAIL";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "JUST VISITING";
            }
            if ((player2Position == 10) && (turn == 2) && (player2Jail))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position10.png");
                labelPropertyCost.Text = "PLAYER 2 - ARRESTED";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "PRISON";
            }
            if ((player2Position == 11) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position11.png");
                labelPropertyCost.Text = "COST: £140";
                labelPropertyRent.Text = "RENT :£10";
                groupBoxStart.Text = "PALL MALL";
            }
            if ((player2Position == 12) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position12.png");
                labelPropertyCost.Text = "COST: £150";
                labelPropertyRent.Text = "RENT :£15";
                groupBoxStart.Text = "ELECTRIC COMPANY";
            }
            if ((player2Position == 13) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position13.png");
                labelPropertyCost.Text = "COST: £140";
                labelPropertyRent.Text = "RENT :£10";
                groupBoxStart.Text = "WHITEHALL";
            }
            if ((player2Position == 14) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position14.png");
                labelPropertyCost.Text = "COST: £160";
                labelPropertyRent.Text = "RENT :£12";
                groupBoxStart.Text = "NOTRHUMBERLAND AVENUE";
            }
            if ((player2Position == 15) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position15.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "MARYLEBONE STATION";
            }
            if ((player2Position == 16) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position16.png");
                labelPropertyCost.Text = "COST: £180";
                labelPropertyRent.Text = "RENT :£14";
                groupBoxStart.Text = "BOW STREET";
            }
            if ((player2Position == 17) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position17.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player2Position == 18) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position18.png");
                labelPropertyCost.Text = "COST: £180";
                labelPropertyRent.Text = "RENT :£14";
                groupBoxStart.Text = "MARLBOROUGH STREET";
            }
            if ((player2Position == 19) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position19.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£16";
                groupBoxStart.Text = "VINE STREET";
            }
            if ((player2Position == 20) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position20.png");
                labelPropertyCost.Text = "DRINK SOME COFFEE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "FREE PARKING SQUARE";
            }
            if ((player2Position == 21) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position21.png");
                labelPropertyCost.Text = "COST: £220";
                labelPropertyRent.Text = "RENT :£18";
                groupBoxStart.Text = "STRAND";
            }
            if ((player2Position == 22) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position22.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player2Position == 23) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position23.png");
                labelPropertyCost.Text = "COST: £220";
                labelPropertyRent.Text = "RENT :£18";
                groupBoxStart.Text = "FLEET STREET";
            }
            if ((player2Position == 24) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position24.png");
                labelPropertyCost.Text = "COST: £240";
                labelPropertyRent.Text = "RENT :£20";
                groupBoxStart.Text = "TRAFALGAR SQUARE";
            }
            if ((player2Position == 25) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position25.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "FENCHURCH STREET STATION";
            }
            if ((player2Position == 26) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position26.png");
                labelPropertyCost.Text = "COST: £260";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "LEICESTER SQUARE";
            }
            if ((player2Position == 27) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position27.png");
                labelPropertyCost.Text = "COST: £260";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "COVENTRY STREET";
            }
            if ((player2Position == 28) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position28.png");
                labelPropertyCost.Text = "COST: £150";
                labelPropertyRent.Text = "RENT :£15";
                groupBoxStart.Text = "WATER WORKS";
            }
            if ((player2Position == 29) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position29.png");
                labelPropertyCost.Text = "COST: £280";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "PICCADILLY";
            }
            if ((player2Position == 30) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position30.png");
                labelPropertyCost.Text = "GO TO JAIL";
                labelPropertyRent.Text = "AND STAY THERE";
                groupBoxStart.Text = "GO TO JAIL SQUARE";
            }
            if ((player2Position == 31) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position31.png");
                labelPropertyCost.Text = "COST: £300";
                labelPropertyRent.Text = "RENT :£26";
                groupBoxStart.Text = "REGENT STREET";
            }
            if ((player2Position == 32) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position32.png");
                labelPropertyCost.Text = "COST: £300";
                labelPropertyRent.Text = "RENT :£26";
                groupBoxStart.Text = "OXFORD STREET";
            }
            if ((player2Position == 33) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position33.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player2Position == 34) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position34.png");
                labelPropertyCost.Text = "COST: £320";
                labelPropertyRent.Text = "RENT :£28";
                groupBoxStart.Text = "BOND STREET";
            }
            if ((player2Position == 35) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position35.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "LIVERPOOL STREET STATION";
            }
            if ((player2Position == 36) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position36.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player2Position == 37) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position37.png");
                labelPropertyCost.Text = "COST: £350";
                labelPropertyRent.Text = "RENT :£35";
                groupBoxStart.Text = "PARK LANE";
            }
            if ((player2Position == 38) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position38.png");
                labelPropertyCost.Text = "PAY: £100";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "SUPER TAX";
            }
            if ((player2Position == 39) && (turn == 2))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position39.png");
                labelPropertyCost.Text = "COST: £400";
                labelPropertyRent.Text = "RENT :£50";
                groupBoxStart.Text = "MAYFAIR";
            }
            propertyOwnership();
        }

        public void currentSquarePlayer3()
        {
            if ((player3Position == 0) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position0.png");
                labelPropertyCost.Text = "COLLECT: £200";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "GO SQUARE";
            }
            if ((player3Position == 1) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position1.png");
                labelPropertyCost.Text = "COST: £60";
                labelPropertyRent.Text = "RENT :£2";
                groupBoxStart.Text = "OLD KENT ROAD";
            }
            if ((player3Position == 2) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position2.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player3Position == 3) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position3.png");
                labelPropertyCost.Text = "COST: £60";
                labelPropertyRent.Text = "RENT :£4";
                groupBoxStart.Text = "WHITE CHAPEL ROAD";
            }
            if ((player3Position == 4) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position4.png");
                labelPropertyCost.Text = "PAY: £200";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "INCOME TAX";
            }
            if ((player3Position == 5) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position5.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "KING CROSS STATION";
            }
            if ((player3Position == 6) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position6.png");
                labelPropertyCost.Text = "COST: £100";
                labelPropertyRent.Text = "RENT :£6";
                groupBoxStart.Text = "THE ANGEL ISLINGTON";
            }
            if ((player3Position == 7) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position7.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player3Position == 8) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position8.png");
                labelPropertyCost.Text = "COST: £100";
                labelPropertyRent.Text = "RENT :£6";
                groupBoxStart.Text = "EUSTON ROAD";
            }
            if ((player3Position == 9) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position9.png");
                labelPropertyCost.Text = "COST: £120";
                labelPropertyRent.Text = "RENT :£8";
                groupBoxStart.Text = "PENTONVILLE ROAD";
            }
            if ((player3Position == 10) && (turn == 3) && (!player3Jail))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position10.png");
                labelPropertyCost.Text = "PLAYER 3 - VISITS JAIL";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "JUST VISITING";
            }
            if ((player3Position == 10) && (turn == 3) && (player3Jail))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position10.png");
                labelPropertyCost.Text = "PLAYER 3 - ARRESTED";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "PRISON";
            }
            if ((player3Position == 11) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position11.png");
                labelPropertyCost.Text = "COST: £140";
                labelPropertyRent.Text = "RENT :£10";
                groupBoxStart.Text = "PALL MALL";
            }
            if ((player3Position == 12) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position12.png");
                labelPropertyCost.Text = "COST: £150";
                labelPropertyRent.Text = "RENT :£15";
                groupBoxStart.Text = "ELECTRIC COMPANY";
            }
            if ((player3Position == 13) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position13.png");
                labelPropertyCost.Text = "COST: £140";
                labelPropertyRent.Text = "RENT :£10";
                groupBoxStart.Text = "WHITEHALL";
            }
            if ((player3Position == 14) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position14.png");
                labelPropertyCost.Text = "COST: £160";
                labelPropertyRent.Text = "RENT :£12";
                groupBoxStart.Text = "NOTRHUMBERLAND AVENUE";
            }
            if ((player3Position == 15) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position15.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "MARYLEBONE STATION";
            }
            if ((player3Position == 16) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position16.png");
                labelPropertyCost.Text = "COST: £180";
                labelPropertyRent.Text = "RENT :£14";
                groupBoxStart.Text = "BOW STREET";
            }
            if ((player3Position == 17) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position17.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player3Position == 18) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position18.png");
                labelPropertyCost.Text = "COST: £180";
                labelPropertyRent.Text = "RENT :£14";
                groupBoxStart.Text = "MARLBOROUGH STREET";
            }
            if ((player3Position == 19) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position19.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£16";
                groupBoxStart.Text = "VINE STREET";
            }
            if ((player3Position == 20) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position20.png");
                labelPropertyCost.Text = "DRINK SOME COFFEE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "FREE PARKING SQUARE";
            }
            if ((player3Position == 21) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position21.png");
                labelPropertyCost.Text = "COST: £220";
                labelPropertyRent.Text = "RENT :£18";
                groupBoxStart.Text = "STRAND";
            }
            if ((player3Position == 22) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position22.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player3Position == 23) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position23.png");
                labelPropertyCost.Text = "COST: £220";
                labelPropertyRent.Text = "RENT :£18";
                groupBoxStart.Text = "FLEET STREET";
            }
            if ((player3Position == 24) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position24.png");
                labelPropertyCost.Text = "COST: £240";
                labelPropertyRent.Text = "RENT :£20";
                groupBoxStart.Text = "TRAFALGAR SQUARE";
            }
            if ((player3Position == 25) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position25.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "FENCHURCH STREET STATION";
            }
            if ((player3Position == 26) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position26.png");
                labelPropertyCost.Text = "COST: £260";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "LEICESTER SQUARE";
            }
            if ((player3Position == 27) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position27.png");
                labelPropertyCost.Text = "COST: £260";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "COVENTRY STREET";
            }
            if ((player3Position == 28) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position28.png");
                labelPropertyCost.Text = "COST: £150";
                labelPropertyRent.Text = "RENT :£15";
                groupBoxStart.Text = "WATER WORKS";
            }
            if ((player3Position == 29) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position29.png");
                labelPropertyCost.Text = "COST: £280";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "PICCADILLY";
            }
            if ((player3Position == 30) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position30.png");
                labelPropertyCost.Text = "GO TO JAIL";
                labelPropertyRent.Text = "AND STAY THERE";
                groupBoxStart.Text = "GO TO JAIL SQUARE";
            }
            if ((player3Position == 31) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position31.png");
                labelPropertyCost.Text = "COST: £300";
                labelPropertyRent.Text = "RENT :£26";
                groupBoxStart.Text = "REGENT STREET";
            }
            if ((player3Position == 32) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position32.png");
                labelPropertyCost.Text = "COST: £300";
                labelPropertyRent.Text = "RENT :£26";
                groupBoxStart.Text = "OXFORD STREET";
            }
            if ((player3Position == 33) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position33.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player3Position == 34) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position34.png");
                labelPropertyCost.Text = "COST: £320";
                labelPropertyRent.Text = "RENT :£28";
                groupBoxStart.Text = "BOND STREET";
            }
            if ((player3Position == 35) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position35.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "LIVERPOOL STREET STATION";
            }
            if ((player3Position == 36) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position36.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player3Position == 37) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position37.png");
                labelPropertyCost.Text = "COST: £350";
                labelPropertyRent.Text = "RENT :£35";
                groupBoxStart.Text = "PARK LANE";
            }
            if ((player3Position == 38) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position38.png");
                labelPropertyCost.Text = "PAY: £100";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "SUPER TAX";
            }
            if ((player3Position == 39) && (turn == 3))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position39.png");
                labelPropertyCost.Text = "COST: £400";
                labelPropertyRent.Text = "RENT :£50";
                groupBoxStart.Text = "MAYFAIR";
            }
            propertyOwnership();
        }

        public void currentSquarePlayer4()
        {
            if ((player4Position == 0) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position0.png");
                labelPropertyCost.Text = "COLLECT: £200";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "GO SQUARE";
            }
            if ((player4Position == 1) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position1.png");
                labelPropertyCost.Text = "COST: £60";
                labelPropertyRent.Text = "RENT :£2";
                groupBoxStart.Text = "OLD KENT ROAD";
            }
            if ((player4Position == 2) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position2.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player4Position == 3) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position3.png");
                labelPropertyCost.Text = "COST: £60";
                labelPropertyRent.Text = "RENT :£4";
                groupBoxStart.Text = "WHITE CHAPEL ROAD";
            }
            if ((player4Position == 4) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position4.png");
                labelPropertyCost.Text = "PAY: £200";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "INCOME TAX";
            }
            if ((player4Position == 5) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position5.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "KING CROSS STATION";
            }
            if ((player4Position == 6) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position6.png");
                labelPropertyCost.Text = "COST: £100";
                labelPropertyRent.Text = "RENT :£6";
                groupBoxStart.Text = "THE ANGEL ISLINGTON";
            }
            if ((player4Position == 7) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position7.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player4Position == 8) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position8.png");
                labelPropertyCost.Text = "COST: £100";
                labelPropertyRent.Text = "RENT :£6";
                groupBoxStart.Text = "EUSTON ROAD";
            }
            if ((player4Position == 9) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position9.png");
                labelPropertyCost.Text = "COST: £120";
                labelPropertyRent.Text = "RENT :£8";
                groupBoxStart.Text = "PENTONVILLE ROAD";
            }
            if ((player4Position == 10) && (turn == 4) && (!player4Jail))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position10.png");
                labelPropertyCost.Text = "PLAYER 4 - VISITS JAIL";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "JUST VISITING";
            }
            if ((player4Position == 10) && (turn == 4) && (player4Jail))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position10.png");
                labelPropertyCost.Text = "PLAYER 4 - ARRESTED";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "PRISON";
            }
            if ((player4Position == 11) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position11.png");
                labelPropertyCost.Text = "COST: £140";
                labelPropertyRent.Text = "RENT :£10";
                groupBoxStart.Text = "PALL MALL";
            }
            if ((player4Position == 12) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position12.png");
                labelPropertyCost.Text = "COST: £150";
                labelPropertyRent.Text = "RENT :£15";
                groupBoxStart.Text = "ELECTRIC COMPANY";
            }
            if ((player4Position == 13) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position13.png");
                labelPropertyCost.Text = "COST: £140";
                labelPropertyRent.Text = "RENT :£10";
                groupBoxStart.Text = "WHITEHALL";
            }
            if ((player4Position == 14) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position14.png");
                labelPropertyCost.Text = "COST: £160";
                labelPropertyRent.Text = "RENT :£12";
                groupBoxStart.Text = "NOTRHUMBERLAND AVENUE";
            }
            if ((player4Position == 15) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position15.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "MARYLEBONE STATION";
            }
            if ((player4Position == 16) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position16.png");
                labelPropertyCost.Text = "COST: £180";
                labelPropertyRent.Text = "RENT :£14";
                groupBoxStart.Text = "BOW STREET";
            }
            if ((player4Position == 17) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position17.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player4Position == 18) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position18.png");
                labelPropertyCost.Text = "COST: £180";
                labelPropertyRent.Text = "RENT :£14";
                groupBoxStart.Text = "MARLBOROUGH STREET";
            }
            if ((player4Position == 19) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position19.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£16";
                groupBoxStart.Text = "VINE STREET";
            }
            if ((player4Position == 20) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position20.png");
                labelPropertyCost.Text = "DRINK SOME COFFEE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "FREE PARKING SQUARE";
            }
            if ((player4Position == 21) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position21.png");
                labelPropertyCost.Text = "COST: £220";
                labelPropertyRent.Text = "RENT :£18";
                groupBoxStart.Text = "STRAND";
            }
            if ((player4Position == 22) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position22.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player4Position == 23) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position23.png");
                labelPropertyCost.Text = "COST: £220";
                labelPropertyRent.Text = "RENT :£18";
                groupBoxStart.Text = "FLEET STREET";
            }
            if ((player4Position == 24) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position24.png");
                labelPropertyCost.Text = "COST: £240";
                labelPropertyRent.Text = "RENT :£20";
                groupBoxStart.Text = "TRAFALGAR SQUARE";
            }
            if ((player4Position == 25) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position25.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "FENCHURCH STREET STATION";
            }
            if ((player4Position == 26) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position26.png");
                labelPropertyCost.Text = "COST: £260";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "LEICESTER SQUARE";
            }
            if ((player4Position == 27) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position27.png");
                labelPropertyCost.Text = "COST: £260";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "COVENTRY STREET";
            }
            if ((player4Position == 28) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position28.png");
                labelPropertyCost.Text = "COST: £150";
                labelPropertyRent.Text = "RENT :£15";
                groupBoxStart.Text = "WATER WORKS";
            }
            if ((player4Position == 29) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position29.png");
                labelPropertyCost.Text = "COST: £280";
                labelPropertyRent.Text = "RENT :£22";
                groupBoxStart.Text = "PICCADILLY";
            }
            if ((player4Position == 30) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position30.png");
                labelPropertyCost.Text = "GO TO JAIL";
                labelPropertyRent.Text = "AND STAY THERE";
                groupBoxStart.Text = "GO TO JAIL SQUARE";
            }
            if ((player4Position == 31) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position31.png");
                labelPropertyCost.Text = "COST: £300";
                labelPropertyRent.Text = "RENT :£26";
                groupBoxStart.Text = "REGENT STREET";
            }
            if ((player4Position == 32) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position32.png");
                labelPropertyCost.Text = "COST: £300";
                labelPropertyRent.Text = "RENT :£26";
                groupBoxStart.Text = "OXFORD STREET";
            }
            if ((player4Position == 33) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position33.png");
                labelPropertyCost.Text = "OPEN A CHEST";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "COMMUNITY CHEST";
            }
            if ((player4Position == 34) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position34.png");
                labelPropertyCost.Text = "COST: £320";
                labelPropertyRent.Text = "RENT :£28";
                groupBoxStart.Text = "BOND STREET";
            }
            if ((player4Position == 35) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position35.png");
                labelPropertyCost.Text = "COST: £200";
                labelPropertyRent.Text = "RENT :£25";
                groupBoxStart.Text = "LIVERPOOL STREET STATION";
            }
            if ((player4Position == 36) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position36.png");
                labelPropertyCost.Text = "GET A CHANCE";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "CHANCE CARD";
            }
            if ((player4Position == 37) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position37.png");
                labelPropertyCost.Text = "COST: £350";
                labelPropertyRent.Text = "RENT :£35";
                groupBoxStart.Text = "PARK LANE";
            }
            if ((player4Position == 38) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position38.png");
                labelPropertyCost.Text = "PAY: £100";
                labelPropertyRent.Text = "";
                groupBoxStart.Text = "SUPER TAX";
            }
            if ((player4Position == 39) && (turn == 4))
            {
                pictureBoxPropertyImage.Image = Image.FromFile("position39.png");
                labelPropertyCost.Text = "COST: £400";
                labelPropertyRent.Text = "RENT :£50";
                groupBoxStart.Text = "MAYFAIR";
            }
            propertyOwnership();
        }

        // all logic related to buying and paying rent
        public void PropertyBuyPayRentP1()
        {
            if ((properties[player1Position] == 0) && (turn == 1))
            {
                if (player1Money < prices[player1Position])
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PURCHASE");
                else
                {
                    player1Money -= prices[player1Position];
                    properties[player1Position] = 1;
                    MessageBox.Show("PLAYER 1 - You got it!! You own this property", "PROPERTY CARD");
                    labelP1Money.Text = "£" + player1Money.ToString();
                    player1Property += 1;
                    labelP1Property.Text = player1Property.ToString();
                }
            }
            if ((properties[player1Position] == 2) && (turn == 1))
            {
                if (player1Money < rent[player1Position])
                {
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 1 - GAME OVER");
                    player2Money += player1Money;
                    player1Money = 0;
                    labelP2Money.Text = "£" + player2Money.ToString();
                    labelP1Money.Visible = false;
                    labelP1Property.Visible = false;
                    labelPlayer1Piece.Visible = false;
                    labelMoney1.Text = "Game";
                    labelPlayer1Property.Text = "Over";
                    p1GameOver = true;
                    returnPropertiesP1();
                }
                else
                {
                    player1Money -= rent[player1Position];
                    player2Money += rent[player1Position];
                    MessageBox.Show("PLAYER 1 - You have paid rent to PLAYER 2", "RENT PAID");
                    labelP1Money.Text = "£" + player1Money.ToString();
                    labelP2Money.Text = "£" + player2Money.ToString();
                }
            }
            if ((properties[player1Position] == 3) && (turn == 1))
            {
                if (player1Money < rent[player1Position])
                {
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 1 - GAME OVER");
                    player3Money += player1Money;
                    player1Money = 0;
                    labelP3Money.Text = "£" + player3Money.ToString();
                    labelP1Money.Visible = false;
                    labelP1Property.Visible = false;
                    labelPlayer1Piece.Visible = false;
                    labelMoney1.Text = "Game";
                    labelPlayer1Property.Text = "Over";
                    p1GameOver = true;
                    returnPropertiesP1();
                }
                else
                {
                    player1Money -= rent[player1Position];
                    player3Money += rent[player1Position];
                    MessageBox.Show("PLAYER 1 - You have paid rent to PLAYER 3", "RENT PAID");
                    labelP1Money.Text = "£" + player1Money.ToString();
                    labelP3Money.Text = "£" + player3Money.ToString();
                }
            }
            if ((properties[player1Position] == 4) && (turn == 1))
            {
                if (player1Money < rent[player1Position])
                {
                    MessageBox.Show("PLAYER 1 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 1 - GAME OVER");
                    player4Money += player1Money;
                    player1Money = 0;
                    labelP4Money.Text = "£" + player4Money.ToString();
                    labelP1Money.Visible = false;
                    labelP1Property.Visible = false;
                    labelPlayer1Piece.Visible = false;
                    labelMoney1.Text = "Game";
                    labelPlayer1Property.Text = "Over";
                    p1GameOver = true;
                    returnPropertiesP1();
                }
                else
                {
                    player1Money -= rent[player1Position];
                    player4Money += rent[player1Position];
                    MessageBox.Show("PLAYER 1 - You have paid rent to PLAYER 4", "RENT PAID");
                    labelP1Money.Text = "£" + player1Money.ToString();
                    labelP4Money.Text = "£" + player4Money.ToString();
                }
            }
        }

        public void PropertyBuyPayRentP2()
        {
            if ((properties[player2Position] == 0) && (turn == 2))
            {
                if (player2Money < prices[player2Position])
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PURCHASE");
                else
                {
                    player2Money -= prices[player2Position];
                    properties[player2Position] = 2;
                    MessageBox.Show("PLAYER 2 - You got it!! You own this property", "PROPERTY CARD");
                    labelP2Money.Text = "£" + player2Money.ToString();
                    player2Property += 1;
                    labelP2Property.Text = player2Property.ToString();
                }
            }
            if ((properties[player2Position] == 1) && (turn == 2))
            {
                if (player2Money < rent[player2Position])
                {
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 2 - GAME OVER");
                    player1Money += player2Money;
                    player2Money = 0;
                    labelP1Money.Text = "£" + player1Money.ToString();
                    labelP2Money.Visible = false;
                    labelP2Property.Visible = false;
                    labelPlayer2Piece.Visible = false;
                    labelMoney2.Text = "Game";
                    labelPlayer2Property.Text = "Over";
                    p2GameOver = true;
                    returnPropertiesP2();
                }
                else
                {
                    player2Money -= rent[player2Position];
                    player1Money += rent[player2Position];
                    MessageBox.Show("PLAYER 2 - You have paid rent to PLAYER 1", "RENT PAID");
                    labelP2Money.Text = "£" + player2Money.ToString();
                    labelP1Money.Text = "£" + player1Money.ToString();
                }
            }
            if ((properties[player2Position] == 3) && (turn == 2))
            {
                if (player2Money < rent[player2Position])
                {
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 2 - GAME OVER");
                    player3Money += player2Money;
                    player2Money = 0;
                    labelP3Money.Text = "£" + player3Money.ToString();
                    labelP2Money.Visible = false;
                    labelP2Property.Visible = false;
                    labelPlayer2Piece.Visible = false;
                    labelMoney2.Text = "Game";
                    labelPlayer2Property.Text = "Over";
                    p2GameOver = true;
                    returnPropertiesP2();
                }
                else
                {
                    player2Money -= rent[player2Position];
                    player3Money += rent[player2Position];
                    MessageBox.Show("PLAYER 2 - You have paid rent to PLAYER 3", "RENT PAID");
                    labelP2Money.Text = "£" + player2Money.ToString();
                    labelP3Money.Text = "£" + player3Money.ToString();
                }
            }
            if ((properties[player2Position] == 4) && (turn == 2))
            {
                if (player2Money < rent[player2Position])
                {
                    MessageBox.Show("PLAYER 2 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 2 - GAME OVER");
                    player4Money += player2Money;
                    player2Money = 0;
                    labelP4Money.Text = "£" + player4Money.ToString();
                    labelP2Money.Visible = false;
                    labelP2Property.Visible = false;
                    labelPlayer2Piece.Visible = false;
                    labelMoney2.Text = "Game";
                    labelPlayer2Property.Text = "Over";
                    p2GameOver = true;
                    returnPropertiesP2();
                }
                else
                {
                    player2Money -= rent[player2Position];
                    player4Money += rent[player2Position];
                    MessageBox.Show("PLAYER 2 - You have paid rent to PLAYER 4", "RENT PAID");
                    labelP2Money.Text = "£" + player2Money.ToString();
                    labelP4Money.Text = "£" + player4Money.ToString();
                }
            }
        }

        public void PropertyBuyPayRentP3()
        {
            if ((properties[player3Position] == 0) && (turn == 3))
            {
                if (player3Money < prices[player3Position])
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PURCHASE");
                else
                {
                    player3Money -= prices[player3Position];
                    properties[player3Position] = 3;
                    MessageBox.Show("PLAYER 3 - You got it!! You own this property", "PROPERTY CARD");
                    labelP3Money.Text = "£" + player3Money.ToString();
                    player3Property += 1;
                    labelP3Property.Text = player3Property.ToString();
                }
            }
            if ((properties[player3Position] == 1) && (turn == 3))
            {
                if (player3Money < rent[player3Position])
                {
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 3 - GAME OVER");
                    player1Money += player3Money;
                    player3Money = 0;
                    labelP1Money.Text = "£" + player1Money.ToString();
                    labelP3Money.Visible = false;
                    labelP3Property.Visible = false;
                    labelPlayer3Piece.Visible = false;
                    labelMoney3.Text = "Game";
                    labelPlayer3Property.Text = "Over";
                    p3GameOver = true;
                    returnPropertiesP3();
                }
                else
                {
                    player3Money -= rent[player3Position];
                    player1Money += rent[player3Position];
                    MessageBox.Show("PLAYER 3 - You have paid rent to PLAYER 1", "RENT PAID");
                    labelP3Money.Text = "£" + player3Money.ToString();
                    labelP1Money.Text = "£" + player1Money.ToString();
                }
            }
            if ((properties[player3Position] == 2) && (turn == 3))
            {
                if (player3Money < rent[player3Position])
                {
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 3 - GAME OVER");
                    player2Money += player3Money;
                    player3Money = 0;
                    labelP2Money.Text = "£" + player2Money.ToString();
                    labelP3Money.Visible = false;
                    labelP3Property.Visible = false;
                    labelPlayer3Piece.Visible = false;
                    labelMoney3.Text = "Game";
                    labelPlayer3Property.Text = "Over";
                    p3GameOver = true;
                    returnPropertiesP3();
                }
                else
                {
                    player3Money -= rent[player3Position];
                    player2Money += rent[player3Position];
                    MessageBox.Show("PLAYER 3 - You have paid rent to PLAYER 2", "RENT PAID");
                    labelP3Money.Text = "£" + player3Money.ToString();
                    labelP2Money.Text = "£" + player2Money.ToString();
                }
            }
            if ((properties[player3Position] == 4) && (turn == 3))
            {
                if (player3Money < rent[player3Position])
                {
                    MessageBox.Show("PLAYER 3 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 3 - GAME OVER");
                    player4Money += player3Money;
                    player3Money = 0;
                    labelP4Money.Text = "£" + player4Money.ToString();
                    labelP3Money.Visible = false;
                    labelP3Property.Visible = false;
                    labelPlayer3Piece.Visible = false;
                    labelMoney3.Text = "Game";
                    labelPlayer3Property.Text = "Over";
                    p3GameOver = true;
                    returnPropertiesP3();
                }
                else
                {
                    player3Money -= rent[player3Position];
                    player4Money += rent[player3Position];
                    MessageBox.Show("PLAYER 3 - You have paid rent to PLAYER 4", "RENT PAID");
                    labelP3Money.Text = "£" + player3Money.ToString();
                    labelP4Money.Text = "£" + player4Money.ToString();
                }
            }
        }

        public void PropertyBuyPayRentP4()
        {
            if ((properties[player4Position] == 0) && (turn == 4))
            {
                if (player4Money < prices[player4Position])
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PURCHASE");
                else
                {
                    player4Money -= prices[player4Position];
                    properties[player4Position] = 4;
                    MessageBox.Show("PLAYER 4 - You got it!! You own this property", "PROPERTY CARD");
                    labelP4Money.Text = "£" + player4Money.ToString();
                    player4Property += 1;
                    labelP4Property.Text = player4Property.ToString();
                }
            }
            if ((properties[player4Position] == 1) && (turn == 4))
            {
                if (player4Money < rent[player4Position])
                {
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 4 - GAME OVER");
                    player1Money += player4Money;
                    player4Money = 0;
                    labelP1Money.Text = "£" + player1Money.ToString();
                    labelP4Money.Visible = false;
                    labelP4Property.Visible = false;
                    labelPlayer4Piece.Visible = false;
                    labelMoney4.Text = "Game";
                    labelPlayer4Property.Text = "Over";
                    p4GameOver = true;
                    returnPropertiesP4();
                }
                else
                {
                    player4Money -= rent[player4Position];
                    player1Money += rent[player4Position];
                    MessageBox.Show("PLAYER 4 - You have paid rent to PLAYER 1", "RENT PAID");
                    labelP4Money.Text = "£" + player4Money.ToString();
                    labelP1Money.Text = "£" + player1Money.ToString();
                }
            }
            if ((properties[player4Position] == 2) && (turn == 4))
            {
                if (player4Money < rent[player4Position])
                {
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 4 - GAME OVER");
                    player2Money += player4Money;
                    player4Money = 0;
                    labelP2Money.Text = "£" + player2Money.ToString();
                    labelP4Money.Visible = false;
                    labelP4Property.Visible = false;
                    labelPlayer4Piece.Visible = false;
                    labelMoney4.Text = "Game";
                    labelPlayer4Property.Text = "Over";
                    p4GameOver = true;
                    returnPropertiesP4();
                }
                else
                {
                    player4Money -= rent[player4Position];
                    player2Money += rent[player2Position];
                    MessageBox.Show("PLAYER 4 - You have paid rent to PLAYER 2", "RENT PAID");
                    labelP4Money.Text = "£" + player4Money.ToString();
                    labelP2Money.Text = "£" + player2Money.ToString();
                }
            }
            if ((properties[player4Position] == 3) && (turn == 4))
            {
                if (player4Money < rent[player4Position])
                {
                    MessageBox.Show("PLAYER 4 - You don't have enough money!", "CAN'T PAY RENT");
                    MessageBox.Show("PLAYER 4 - GAME OVER");
                    player3Money += player4Money;
                    player4Money = 0;
                    labelP3Money.Text = "£" + player3Money.ToString();
                    labelP4Money.Visible = false;
                    labelP4Property.Visible = false;
                    labelPlayer4Piece.Visible = false;
                    labelMoney4.Text = "Game";
                    labelPlayer4Property.Text = "Over";
                    p4GameOver = true;
                    returnPropertiesP4();
                }
                else
                {
                    player4Money -= rent[player4Position];
                    player3Money += rent[player4Position];
                    MessageBox.Show("PLAYER 4 - You have paid rent to PLAYER 3", "RENT PAID");
                    labelP4Money.Text = "£" + player4Money.ToString();
                    labelP3Money.Text = "£" + player3Money.ToString();
                }
            }
        }

        // helps to keep display box nice with all the relevant info
        public void propertyOwnership()
        {
            if ((properties[player1Position] == 1) && (turn == 1))
            {
                labelOwnedBy.Text = "PLAYER 1 - YOU OWN IT";
            }
            else if ((properties[player1Position] == 2) && (turn == 1))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 2";
            }
            else if ((properties[player1Position] == 3) && (turn == 1))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 3";
            }
            else if ((properties[player1Position] == 4) && (turn == 1))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 4";
            }
            else if ((properties[player1Position] == -1) && (turn == 1))
            {
                labelOwnedBy.Text = "NOT FOR SALE";
            }
            else if ((properties[player2Position] == 1) && (turn == 2))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 1";
            }
            else if ((properties[player2Position] == 2) && (turn == 2))
            {
                labelOwnedBy.Text = "PLAYER 2 - YOU OWN IT";
            }
            else if ((properties[player2Position] == 3) && (turn == 2))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 3";
            }
            else if ((properties[player2Position] == 4) && (turn == 2))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 4";
            }
            else if ((properties[player2Position] == -1) && (turn == 2))
            {
                labelOwnedBy.Text = "NOT FOR SALE";
            }
            else if ((properties[player3Position] == 1) && (turn == 3))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 1";
            }
            else if ((properties[player3Position] == 2) && (turn == 3))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 2";
            }
            else if ((properties[player3Position] == 3) && (turn == 3))
            {
                labelOwnedBy.Text = "PLAYER 3 - YOU OWN IT";
            }
            else if ((properties[player3Position] == 4) && (turn == 3))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 4";
            }
            else if ((properties[player3Position] == -1) && (turn == 3))
            {
                labelOwnedBy.Text = "NOT FOR SALE";
            }
            else if ((properties[player4Position] == 1) && (turn == 4))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 1";
            }
            else if ((properties[player4Position] == 2) && (turn == 4))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 2";
            }
            else if ((properties[player4Position] == 3) && (turn == 4))
            {
                labelOwnedBy.Text = "OWNED BY PLAYER 3";
            }
            else if ((properties[player4Position] == 4) && (turn == 4))
            {
                labelOwnedBy.Text = "PLAYER 4 - YOU OWN IT";
            }
            else if ((properties[player4Position] == -1) && (turn == 4))
            {
                labelOwnedBy.Text = "NOT FOR SALE";
            }
            else
            {
                labelOwnedBy.Text = "NOT OWNED BY ANYONE";
            }
        }

        // checks if player can roll again considering other possible scenarious - prison time, or maybe game over
        public void rollAgainDice()
        {
            if ((turn == 1) && (!player1Jail) && (!comingOutOfJale) && (!p1GameOver))
            {
                if (dice_throw1 == dice_throw2)
                    rollAgain = true;
            }
            if ((turn == 2) && (!player2Jail) && (!comingOutOfJale) && (!p2GameOver))
            {
                if (dice_throw1 == dice_throw2)
                    rollAgain = true;
            }
            if ((turn == 3) && (!player3Jail) && (!comingOutOfJale) && (!p3GameOver))
            {
                if (dice_throw1 == dice_throw2)
                    rollAgain = true;
            }
            if ((turn == 4) && (!player4Jail) && (!comingOutOfJale) && (!p4GameOver))
            {
                if (dice_throw1 == dice_throw2)
                    rollAgain = true;
            }
        }

        // checks if player is still active or not
        public void checkPlayerStatus()
        {
            if ((turn == 1) && (p1GameOver))
                turn += 1;
            if ((turn == 2) && (p2GameOver))
                turn += 1;
            if ((turn == 3) && (p3GameOver))
                turn += 1;
            if ((turn == 4) && (p4GameOver))
                turn += 1;
        }

        // displays the winner accordingly
        public void endGame()
        {
            if (!p1GameOver)
            {
                for (int i = 0; i < 40; i++)
                {
                    if (properties[i] == 1)
                        properties[i] = 0;
                }
                MessageBox.Show("PLAYER 1 - YOU WON!", "CONGRATULATIONS");
                labelMain.Text = "PLAYER 1 IS THE WINNER! CONGRATS CHAMP!";
                pictureBoxPropertyImage.Image = Image.FromFile("the_champ.png");
                labelPropertyCost.Text = "Got to say it..";
                labelPropertyRent.Text = "You Played Well!";
                groupBoxStart.Text = "CONGRATULATIONS";
                labelOwnedBy.Text = "";
                labelRollDice.Text = "";
            }
            if (!p2GameOver)
            {
                for (int i = 0; i < 40; i++)
                {
                    if (properties[i] == 2)
                        properties[i] = 0;
                }
                MessageBox.Show("PLAYER 2 - YOU WON!", "CONGRATULATIONS");
                labelMain.Text = "PLAYER 2 IS THE WINNER! CONGRATS CHAMP!";
                pictureBoxPropertyImage.Image = Image.FromFile("the_champ.png");
                labelPropertyCost.Text = "Got to say it..";
                labelPropertyRent.Text = "You Played Well!";
                groupBoxStart.Text = "CONGRATULATIONS";
                labelOwnedBy.Text = "";
                labelRollDice.Text = "";
            }
            if (!p3GameOver)
            {
                for (int i = 0; i < 40; i++)
                {
                    if (properties[i] == 3)
                        properties[i] = 0;
                }
                MessageBox.Show("PLAYER 3 - YOU WON!", "CONGRATULATIONS");
                labelMain.Text = "PLAYER 3 IS THE WINNER! CONGRATS CHAMP!";
                pictureBoxPropertyImage.Image = Image.FromFile("the_champ.png");
                labelPropertyCost.Text = "Got to say it..";
                labelPropertyRent.Text = "You Played Well!";
                groupBoxStart.Text = "CONGRATULATIONS";
                labelOwnedBy.Text = "";
                labelRollDice.Text = "";
            }
            if (!p4GameOver)
            {
                for (int i = 0; i < 40; i++)
                {
                    if (properties[i] == 4)
                        properties[i] = 0;
                }
                MessageBox.Show("PLAYER 4 - YOU WON!", "CONGRATULATIONS");
                labelMain.Text = "PLAYER 4 IS THE WINNER! CONGRATS CHAMP!";
                pictureBoxPropertyImage.Image = Image.FromFile("the_champ.png");
                labelPropertyCost.Text = "Got to say it..";
                labelPropertyRent.Text = "You Played Well!";
                groupBoxStart.Text = "CONGRATULATIONS";
                labelOwnedBy.Text = "";
                labelRollDice.Text = "";
            }
            pictureBoxDice1.Visible = false;
            pictureBoxDice2.Visible = false;
            buttonRollDice.Visible = false;
            buttonEndTurn.Visible = false;
            buttonBuyProperty.Visible = false;
            buttonPayRent.Visible = false;
        }

        /* public void AddingTransparency()
         {
             if (start)
             {
                 pictureBoxStart.Controls.Add(labelPlayer1Piece);
                 labelPlayer1Piece.BackColor = Color.Transparent;
             }
         } */

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            InitializeMoney();
            InitializeProperty();
            buttonStartGame.Enabled = false;
            buttonStartGame.Visible = false;
            labelMain.Text = "PLAYER 1"; 
            labelRollDice.Text = "Roll The Dice";
            groupBoxStart.Text = "START YOUR JOURNEY"; 
            buttonBuyProperty.Visible = false;
            buttonEndTurn.Visible = true;
            buttonPayRent.Visible = false;
            buttonRollDice.Visible = true;
            buttonSellProperty.Visible = false;
            StartPlayerPieces();
            //AddingTransparency();
        }

        private void buttonRollDice_Click(object sender, EventArgs e)
        {
            Random ran = new Random(); // creates new random generation used for dice

            dice_throw1 = ran.Next(1, 7);
            dice_throw2 = ran.Next(1, 7);

            labelRollDice.Text = "You Rolled " + dice_throw1 + " and " + dice_throw2;

            if (turn == 1) // initiates player1 to act
            {
                player1Position += dice_throw1 + dice_throw2;

                if (dice_throw1 == 1) 
                    pictureBoxDice1.Image = Image.FromFile("d1.png");
                if (dice_throw1 == 2)
                    pictureBoxDice1.Image = Image.FromFile("d2.png");
                if (dice_throw1 == 3)
                    pictureBoxDice1.Image = Image.FromFile("d3.png");
                if (dice_throw1 == 4)
                    pictureBoxDice1.Image = Image.FromFile("d4.png");
                if (dice_throw1 == 5)
                    pictureBoxDice1.Image = Image.FromFile("d5.png");
                if (dice_throw1 == 6)
                    pictureBoxDice1.Image = Image.FromFile("d6.png");
            
                if (dice_throw2 == 1)
                    pictureBoxDice2.Image = Image.FromFile("d1.png");
                if (dice_throw2 == 2)
                    pictureBoxDice2.Image = Image.FromFile("d2.png");
                if (dice_throw2 == 3)
                    pictureBoxDice2.Image = Image.FromFile("d3.png");
                if (dice_throw2 == 4)
                    pictureBoxDice2.Image = Image.FromFile("d4.png");
                if (dice_throw2 == 5)
                    pictureBoxDice2.Image = Image.FromFile("d5.png");
                if (dice_throw2 == 6)
                    pictureBoxDice2.Image = Image.FromFile("d6.png");

                if (player1Jail) // dice logic in case player 1 is in jail
                {
                    if ((dice_throw1 == dice_throw2) && (jailTimeP1 < 3))
                    {
                        player1Jail = false;
                        chance1Jail = false;
                        comingOutOfJale = true;
                        jailTimeP1 = 0;
                    }
                    else if (jailTimeP1 == 3)
                    {
                        if (player1Money < 50)
                        {
                            MessageBox.Show("PLAYER 1 - You don't have enough money to pay!", "CAN'T PAY TO PRISON");
                            MessageBox.Show("PLAYER 1 - GAME OVER");
                            labelP1Money.Visible = false;
                            labelP1Property.Visible = false;
                            labelPlayer1Piece.Visible = false;
                            labelMoney1.Text = "Game";
                            labelPlayer1Property.Text = "Over";
                            p1GameOver = true;
                            returnPropertiesP1();
                            player1Position -= dice_throw1 + dice_throw2;
                        }
                        else
                        {
                            player1Money -= 50;
                            labelP1Money.Text = "£" + player1Money;
                            chance1Jail = false;
                            jailTimeP1 = 0;
                            player1Jail = false;
                            comingOutOfJale = true;
                        }
                    }
                    else
                    {
                        jailTimeP1 += 1;
                        player1Position -= dice_throw1 + dice_throw2;
                    }
                }
                
                if (player1Position > 39) // ensures position is never bigger than 39 (0-39 squares)
                {
                    player1Position -= 40;
                    player1Money += 200;
                    MessageBox.Show("PLAYER 1 - you have collected £200!", "GO SQUARE");
                    labelP1Money.Text = "£" + player1Money.ToString();
                }

                if (!p1GameOver) // updates player1 position on board
                {
                    player1Movement();
                    currentSquarePlayer1();
                }

                if (properties[player1Position] == 0)
                {
                    buttonBuyProperty.Visible = true;
                    buttonEndTurn.Enabled = true;
                }
                else if ((properties[player1Position] > 0) && (properties[player1Position] != 1))
                {
                    buttonPayRent.Visible = true;
                    buttonEndTurn.Visible = false;
                }
                else if (properties[player1Position] == 1)
                {
                    buttonEndTurn.Enabled = true;
                    buttonPayRent.Visible = false;
                }
                else
                    buttonEndTurn.Enabled = true;
            }
            if (turn == 2) // initiates player2 to act
            {
                player2Position += dice_throw1 + dice_throw2;

                if (dice_throw1 == 1)
                    pictureBoxDice1.Image = Image.FromFile("d1.png");
                if (dice_throw1 == 2)
                    pictureBoxDice1.Image = Image.FromFile("d2.png");
                if (dice_throw1 == 3)
                    pictureBoxDice1.Image = Image.FromFile("d3.png");
                if (dice_throw1 == 4)
                    pictureBoxDice1.Image = Image.FromFile("d4.png");
                if (dice_throw1 == 5)
                    pictureBoxDice1.Image = Image.FromFile("d5.png");
                if (dice_throw1 == 6)
                    pictureBoxDice1.Image = Image.FromFile("d6.png");

                if (dice_throw2 == 1)
                    pictureBoxDice2.Image = Image.FromFile("d1.png");
                if (dice_throw2 == 2)
                    pictureBoxDice2.Image = Image.FromFile("d2.png");
                if (dice_throw2 == 3)
                    pictureBoxDice2.Image = Image.FromFile("d3.png");
                if (dice_throw2 == 4)
                    pictureBoxDice2.Image = Image.FromFile("d4.png");
                if (dice_throw2 == 5)
                    pictureBoxDice2.Image = Image.FromFile("d5.png");
                if (dice_throw2 == 6)
                    pictureBoxDice2.Image = Image.FromFile("d6.png");

                if (player2Jail) // dice logic in case player 2 is in jail
                {
                    if ((dice_throw1 == dice_throw2) && (jailTimeP2 < 3))
                    {
                        player2Jail = false;
                        chance2Jail = false;
                        comingOutOfJale = true;
                        jailTimeP2 = 0;
                    }
                    else if (jailTimeP2 == 3)
                    {
                        if (player2Money < 50)
                        {
                            MessageBox.Show("PLAYER 2 - You don't have enough money to pay!", "CAN'T PAY TO PRISON");
                            MessageBox.Show("PLAYER 2 - GAME OVER");
                            labelP2Money.Visible = false;
                            labelP2Property.Visible = false;
                            labelPlayer2Piece.Visible = false;
                            labelMoney2.Text = "Game";
                            labelPlayer2Property.Text = "Over";
                            p2GameOver = true;
                            returnPropertiesP2();
                            player2Position -= dice_throw1 + dice_throw2;
                        }
                        else
                        {
                            player2Money -= 50;
                            labelP2Money.Text = "£" + player2Money;
                            chance2Jail = false;
                            jailTimeP2 = 0;
                            player2Jail = false;
                            comingOutOfJale = true;
                        }
                    }
                    else
                    {
                        jailTimeP2 += 1;
                        player2Position -= dice_throw1 + dice_throw2;
                    }
                }

                if (player2Position > 39) // ensures position is never bigger than 39 (0-39 squares)
                {
                    player2Position -= 40;
                    player2Money += 200;
                    MessageBox.Show("PLAYER 2 - you have collected £200!", "GO SQUARE");
                    labelP2Money.Text = "£" + player2Money.ToString();
                }

                if (!p2GameOver) // intiates player2 movement on board
                {
                    player2Movement();
                    currentSquarePlayer2();
                }

                if (properties[player2Position] == 0)
                {
                    buttonBuyProperty.Visible = true;
                    buttonEndTurn.Enabled = true;
                }
                else if ((properties[player2Position] > 0) && (properties[player2Position] != 2))
                {
                    buttonPayRent.Visible = true;
                    buttonEndTurn.Visible = false;
                }
                else if (properties[player2Position] == 2)
                {
                    buttonEndTurn.Enabled = true;
                    buttonPayRent.Visible = false;
                }
                else buttonEndTurn.Enabled = true;
            }
            if (turn == 3) // initiates player3 to act
            {
                player3Position += dice_throw1 + dice_throw2; // player3 position changes as per dice thrown

                if (dice_throw1 == 1) 
                    pictureBoxDice1.Image = Image.FromFile("d1.png");
                if (dice_throw1 == 2)
                    pictureBoxDice1.Image = Image.FromFile("d2.png");
                if (dice_throw1 == 3)
                    pictureBoxDice1.Image = Image.FromFile("d3.png");
                if (dice_throw1 == 4)
                    pictureBoxDice1.Image = Image.FromFile("d4.png");
                if (dice_throw1 == 5)
                    pictureBoxDice1.Image = Image.FromFile("d5.png");
                if (dice_throw1 == 6)
                    pictureBoxDice1.Image = Image.FromFile("d6.png");

                if (dice_throw2 == 1)
                    pictureBoxDice2.Image = Image.FromFile("d1.png");
                if (dice_throw2 == 2)
                    pictureBoxDice2.Image = Image.FromFile("d2.png");
                if (dice_throw2 == 3)
                    pictureBoxDice2.Image = Image.FromFile("d3.png");
                if (dice_throw2 == 4)
                    pictureBoxDice2.Image = Image.FromFile("d4.png");
                if (dice_throw2 == 5)
                    pictureBoxDice2.Image = Image.FromFile("d5.png");
                if (dice_throw2 == 6)
                    pictureBoxDice2.Image = Image.FromFile("d6.png");

                if (player3Jail)
                {
                    if ((dice_throw1 == dice_throw2) && (jailTimeP3 < 3))
                    {
                        player3Jail = false;
                        chance3Jail = false;
                        comingOutOfJale = true;
                        jailTimeP3 = 0;
                    }
                    else if (jailTimeP3 == 3)
                    {
                        if (player3Money < 50)
                        {
                            MessageBox.Show("PLAYER 3 - You don't have enough money to pay!", "CAN'T PAY TO PRISON");
                            MessageBox.Show("PLAYER 3 - GAME OVER");
                            labelP3Money.Visible = false;
                            labelP3Property.Visible = false;
                            labelPlayer3Piece.Visible = false;
                            labelMoney3.Text = "Game";
                            labelPlayer3Property.Text = "Over";
                            p3GameOver = true;
                            returnPropertiesP3();
                            player3Position -= dice_throw1 + dice_throw2;
                        }
                        else
                        {
                            player3Money -= 50;
                            labelP3Money.Text = "£" + player3Money;
                            chance3Jail = false;
                            jailTimeP3 = 0;
                            player3Jail = false;
                            comingOutOfJale = true;
                        }
                    }
                    else
                    {
                        jailTimeP3 += 1;
                        player3Position -= dice_throw1 + dice_throw2;
                    }
                }

                if (player3Position > 39) // ensures position is never bigger than 39 (0-39 squares)
                {
                    player3Position -= 40;
                    player3Money += 200;
                    MessageBox.Show("PLAYER 3 - you have collected £200!", "GO SQUARE");
                    labelP3Money.Text = "£" + player3Money.ToString();
                }

                if (!p3GameOver) // initiates player3 movement on board
                {
                    player3Movement();
                    currentSquarePlayer3();
                }

                if (properties[player3Position] == 0)
                {
                    buttonBuyProperty.Visible = true;
                    buttonEndTurn.Enabled = true;
                }
                else if ((properties[player3Position] > 0) && (properties[player3Position] != 3))
                {
                    buttonPayRent.Visible = true;
                    buttonEndTurn.Visible = false;
                }
                else if (properties[player3Position] == 3)
                {
                    buttonEndTurn.Enabled = true;
                    buttonPayRent.Visible = false;
                }
                else
                    buttonEndTurn.Enabled = true;
            }
            if (turn == 4) // initiates player4 to act
            {
                player4Position += dice_throw1 + dice_throw2; // // player4 position changes as per dice thrown

                if (dice_throw1 == 1)
                    pictureBoxDice1.Image = Image.FromFile("d1.png");
                if (dice_throw1 == 2)
                    pictureBoxDice1.Image = Image.FromFile("d2.png");
                if (dice_throw1 == 3)
                    pictureBoxDice1.Image = Image.FromFile("d3.png");
                if (dice_throw1 == 4)
                    pictureBoxDice1.Image = Image.FromFile("d4.png");
                if (dice_throw1 == 5)
                    pictureBoxDice1.Image = Image.FromFile("d5.png");
                if (dice_throw1 == 6)
                    pictureBoxDice1.Image = Image.FromFile("d6.png");
            
                if (dice_throw2 == 1)
                    pictureBoxDice2.Image = Image.FromFile("d1.png");
                if (dice_throw2 == 2)
                    pictureBoxDice2.Image = Image.FromFile("d2.png");
                if (dice_throw2 == 3)
                    pictureBoxDice2.Image = Image.FromFile("d3.png");
                if (dice_throw2 == 4)
                    pictureBoxDice2.Image = Image.FromFile("d4.png");
                if (dice_throw2 == 5)
                    pictureBoxDice2.Image = Image.FromFile("d5.png");
                if (dice_throw2 == 6)
                    pictureBoxDice2.Image = Image.FromFile("d6.png");

                if (player4Jail) // dice logic for when player4 is in jail
                {
                    if ((dice_throw1 == dice_throw2) && (jailTimeP4 < 3))
                    {
                        player4Jail = false;
                        chance4Jail = false;
                        comingOutOfJale = true;
                        jailTimeP4 = 0;
                    }
                    else if (jailTimeP4 == 3)
                    {
                        if (player4Money < 50)
                        {
                            MessageBox.Show("PLAYER 4 - You don't have enough money to pay!", "CAN'T PAY TO PRISON");
                            MessageBox.Show("PLAYER 4 - GAME OVER");
                            labelP4Money.Visible = false;
                            labelP4Property.Visible = false;
                            labelPlayer4Piece.Visible = false;
                            labelMoney4.Text = "Game";
                            labelPlayer4Property.Text = "Over";
                            p4GameOver = true;
                            returnPropertiesP4();
                            player4Position -= dice_throw1 + dice_throw2;
                        }
                        else
                        {
                            player4Money -= 50;
                            labelP4Money.Text = "£" + player4Money;
                            chance4Jail = false;
                            jailTimeP4 = 0;
                            player4Jail = false;
                            comingOutOfJale = true;
                        }
                    }
                    else
                    {
                        jailTimeP4 += 1;
                        player4Position -= dice_throw1 + dice_throw2;
                    }
                }

                if (player4Position > 39) // ensures position is never bigger than 39 (0-39 squares)
                {
                    player4Position -= 40;
                    player4Money += 200;
                    MessageBox.Show("PLAYER 4 - you have collected £200!", "GO SQUARE");
                    labelP4Money.Text = "£" + player4Money.ToString();
                }

                if (!p4GameOver) // initiates player4 physical movement on board
                {
                    player4Movement();
                    currentSquarePlayer4();
                }

                if (properties[player4Position] == 0)
                {
                    buttonBuyProperty.Visible = true;
                    buttonEndTurn.Enabled = true;
                }
                else if ((properties[player4Position] > 0) && (properties[player4Position] != 4))
                {
                    buttonPayRent.Visible = true;
                    buttonEndTurn.Visible = false;
                }
                else if (properties[player4Position] == 4)
                {
                    buttonEndTurn.Enabled = true;
                    buttonPayRent.Visible = false;
                }
                else
                    buttonEndTurn.Enabled = true;
            }
            buttonRollDice.Enabled = false;

        }

        private void buttonEndTurn_Click(object sender, EventArgs e)
        {
            if (player1Position == 30)
            {
                labelPlayer1Piece.Top = pictureBoxJail.Top + 3;
                labelPlayer1Piece.Left = pictureBoxJail.Left + 35;
                player1Position = 10;
                player1Jail = true;
            }
            if (player2Position == 30)
            {
                labelPlayer2Piece.Top = pictureBoxJail.Top + 3;
                labelPlayer2Piece.Left = pictureBoxJail.Left + 72;
                player2Position = 10;
                player2Jail = true;
            }
            if (player3Position == 30)
            {
                labelPlayer3Piece.Top = pictureBoxJail.Top + 40;
                labelPlayer3Piece.Left = pictureBoxJail.Left + 35;
                player3Position = 10;
                player3Jail = true;
            }
            if (player4Position == 30)
            {
                labelPlayer4Piece.Top = pictureBoxJail.Top + 40;
                labelPlayer4Piece.Left = pictureBoxJail.Left + 72;
                player4Position = 10;
                player4Jail = true;
            }

            rollAgainDice();
            if (rollAgain)
            {
                labelRollDice.Text = dice_throw1 + " and " + dice_throw2 + " Are Doubles - Roll Again";
                rollAgain = false;
            }
            else
            {
                labelRollDice.Text = "Roll The Dice";
                turn += 1;
            }

            if (turn > 4) // ensures player turns count correctly
                turn -= 4; // takes off 4 each time turn is 5 - resets it back to 1

            checkPlayerStatus();

            if (turn > 4)
                turn -= 4;

            checkPlayerStatus();

            if (turn == 1)
            {
                labelMain.Text = "PLAYER 1";
                currentSquarePlayer1();
                propertyOwnership();
            }
            if (turn == 2)
            {
                labelMain.Text = "PLAYER 2";
                currentSquarePlayer2();
                propertyOwnership();
            }
            if (turn == 3)
            {
                labelMain.Text = "PLAYER 3";
                currentSquarePlayer3();
                propertyOwnership();
            }
            if (turn == 4)
            {
                labelMain.Text = "PLAYER 4";
                currentSquarePlayer4();
                propertyOwnership();
            }
            comingOutOfJale = false;
            buttonRollDice.Enabled = true;
            buttonEndTurn.Enabled = false;
            buttonBuyProperty.Visible = false;
            buttonPayRent.Visible = false;

            if (countGameOver == 3)
                endGame();
        }

        private void buttonBuyProperty_Click(object sender, EventArgs e)
        {
            if (turn == 1)
                PropertyBuyPayRentP1();
            if (turn == 2)
                PropertyBuyPayRentP2();
            if (turn == 3)
                PropertyBuyPayRentP3();
            if (turn == 4)
                PropertyBuyPayRentP4();
            propertyOwnership();
            buttonBuyProperty.Visible = false;
        }

        private void buttonPayRent_Click(object sender, EventArgs e)
        {
            if (turn == 1)
                PropertyBuyPayRentP1();
            if (turn == 2)
                PropertyBuyPayRentP2();
            if (turn == 3)
                PropertyBuyPayRentP3();
            if (turn == 4)
                PropertyBuyPayRentP4();
            buttonPayRent.Visible = false;
            buttonEndTurn.Visible = true;
            buttonEndTurn.Enabled = true;
        }
    }
}
