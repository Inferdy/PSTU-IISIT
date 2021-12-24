using ProductionSystem;

namespace DesktopApp
{
    public partial class Form1 : Form
    {
        private Asker asker;
        private Printer printer;
        private Printer logger;
        private object locker = new object();

        public Form1()
        {
            InitializeComponent();

            asker = new Asker(yesButton, noButton, idkButton, questionTextbox);
            printer = new Printer(infoTextbox);
            logger = new Printer(logTextbox);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    pathTextbox.Text = openFileDialog.FileName;
            }
        }

        private async void startButton_Click(object sender, EventArgs e)
        {
            IProductionSystem productionSystem = new ProductionSystem.ProductionSystem(pathTextbox.Text);

            infoTextbox.Clear();
            logTextbox.Clear();
            questionTextbox.Clear();

            if (Monitor.TryEnter(locker))
                try
                {
                    await Task.Run(() =>
                    {
                        productionSystem.Run(printer, logger, asker);
                    });

                    MessageBox.Show("Ready");
                }
                finally
                {
                    Monitor.Exit(locker);
                }
        }

        private class Asker : IAsker
        {
            private Button yesButton;
            private Button noButton;
            private Button idkButton;
            private RichTextBox textBox;

            private TaskCompletionSource<string>? tcs = null;

            public Asker(Button yesButton, Button noButton, Button idkButton, RichTextBox textBox)
            {
                this.yesButton = yesButton;
                this.noButton = noButton;
                this.idkButton = idkButton;
                this.textBox = textBox;

                Wire();
            }

            private void Wire()
            {
                yesButton.Click += YesButton_Click;
                noButton.Click += NoButton_Click;
                idkButton.Click += IdkButton_Click;
            }

            private void IdkButton_Click(object? sender, EventArgs e)
            {
                tcs?.TrySetResult("да");
            }

            private void NoButton_Click(object? sender, EventArgs e)
            {
                tcs?.TrySetResult("нет");
            }

            private void YesButton_Click(object? sender, EventArgs e)
            {
                tcs?.TrySetResult("н/з");
            }

            public string Ask(string question)
            {
                tcs = new TaskCompletionSource<string>();

                textBox.Invoke(() =>
                {
                    textBox.Text = question;
                });

                yesButton.Invoke(() =>
                {
                    yesButton.Enabled = true;
                });

                noButton.Invoke(() =>
                {
                    noButton.Enabled = true;
                });

                idkButton.Invoke(() =>
                {
                    idkButton.Enabled = true;
                });

                string result = tcs.Task.Result;

                yesButton.Invoke(() =>
                {
                    yesButton.Enabled = false;
                });

                noButton.Invoke(() =>
                {
                    noButton.Enabled = false;
                });

                idkButton.Invoke(() =>
                {
                    idkButton.Enabled = false;
                });

                return result;
            }
        }

        private class Printer : IPrinter
        {
            RichTextBox textBox;

            public Printer(RichTextBox textBox)
            {
                this.textBox = textBox;
            }

            public void Print(string value)
            {
                textBox.Invoke(() =>
                {
                    textBox.AppendText(value);
                });
            }

            public void PrintLine(string value)
            {
                textBox.Invoke(() =>
                {
                    textBox.AppendText(value);
                    textBox.AppendText("\n");
                });
            }

            public void PrintLine()
            {
                textBox.Invoke(() =>
                {
                    textBox.AppendText("\n");
                });
            }
        }
    }
}