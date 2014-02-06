using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GissaTalet.Model;

namespace GissaTalet
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["secret"] == null)
            {
                var secretNumber = new SecretNumber();
                Session["secret"] = secretNumber;
            }
            GuessNumberTextbox.Focus();
        }

        protected void GuessButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var secretNumber = Session["secret"] as SecretNumber;
                var guess = Int32.Parse(GuessNumberTextbox.Text);

                if (secretNumber.CanMakeGuess)
                {
                    var outcome = secretNumber.MakeGuess(guess);

                    var resultString = string.Empty;

                    foreach (int number in secretNumber.PreviousGuesses)
                    {
                        resultString += string.Format("{0}, ", number);
                    }

                    switch (outcome)
                    {
                        case Outcome.High:
                            resultString += "För högt!";
                            GuessNumberTextbox.Focus();
                            break;

                        case Outcome.Low:
                            resultString += "För lågt!";
                            GuessNumberTextbox.Focus();
                            break;

                        case Outcome.Correct:
                            SuccessPanel.Visible = true;
                            SuccessLabel.Text = string.Format("Rätt! Du gissade rätt på {0} gissningar!", secretNumber.Count);
                            GuessNumberTextbox.Enabled = false;
                            GuessButton.Enabled = false;
                            NewSecretNumberButton.Visible = true;
                            NewSecretNumberButton.Focus();
                            break;

                        case Outcome.PreviousGuess:
                            resultString += "Samma som tidigare gissning!";
                            break;

                        case Outcome.NoMoreGuesses:
                            NoMoreGuessesPanel.Visible = true;
                            NoMoreGuessesLabel.Text = string.Format("Inga fler gissningar! Det rätta talet var {0} !", secretNumber.Number);
                            NewSecretNumberButton.Visible = true;
                            NewSecretNumberButton.Focus();
                            GuessNumberTextbox.Enabled = false;
                            GuessButton.Enabled = false;
                            break;

                        default:
                            throw new NotImplementedException();
                    }

                    GuessedNumbersLabel.Text = resultString;
                    ResultPlaceholder.Visible = true;
                }
            }
        }

        protected void NewSecretNumberButton_Click(object sender, EventArgs e)
        {
            var secretNumber = Session["secret"] as SecretNumber;
            secretNumber.Initialize();
        }
    }
}