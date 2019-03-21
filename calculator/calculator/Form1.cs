using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnInput_Click(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            if (currentButton != null && currentButton.Tag != null)
            {
                string input = currentButton.Tag.ToString();
                textBox1.Text = textBox1.Text + input;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string express = textBox1.Text ?? string.Empty;
            if (!string.IsNullOrEmpty(express))
            {
                textBox1.Text = express.Substring(0, express.Length - 1);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = string.Empty;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                string result = Identify(textBox1.Text);
                result = getResult(result);
                textBox2.Text = result;
            }
            catch //(Exception ex)
            {
                MessageBox.Show("输入不合法,请重新输入!");
                textBox1.Text = textBox2.Text = string.Empty;
            }
        }
        private int Priority(char c)
        {
            if (c == '+' || c == '-')
                return 0;
            else if (c == '*' || c == '/' || c == '%')
                return 1;
            else if (c == '^'|| c=='!'|| c== '√')
                return 2;
            else
                return 3;
        }

        private string Identify(string strExpression)
        {
            Stack stack = new Stack();
            StringBuilder st = new StringBuilder();
            char c = ' ';

            StringBuilder sb = new StringBuilder(strExpression);
            for (int i = 0; i < sb.Length; i++)
            {
                if (char.IsDigit(sb[i]) || sb[i] == '.')
                {
                    st.Append(sb[i]);
                }
                else if (sb[i] == '+' || sb[i] == '-' || sb[i] == '*' || sb[i] == '/' || sb[i] == '%' || sb[i] == '^'||sb[i]=='!')
                {
                    while (stack.Count > 0)
                    {
                        c = (char)stack.Pop();
                        if (c == '(')
                        {
                            stack.Push(c);
                            break;
                        }
                        else
                        {
                            if (Priority(c) < Priority(sb[i]))
                            {
                                stack.Push(c);
                                break;
                            }
                            else
                            {
                                st.Append(' ');
                                st.Append(c);
                            }
                        }
                    }
                    stack.Push(sb[i]);
                    st.Append(' ');
                }
                else if (sb[i] == '(')
                {
                    stack.Push('(');
                }
                else if (sb[i] == ')')
                {
                    while (stack.Count > 0)
                    {
                        c = (char)stack.Pop();
                        if (c != '(')
                        {
                            st.Append(' ');
                            st.Append(c);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    st.Append(' ');
                    st.Append(sb[i]);
                }
            }
            while (stack.Count > 0)
            {
                st.Append(' ');
                st.Append(stack.Pop());
            }
            return st.ToString();
        }

        private string getResult(string strExpression)
        {
            Stack stack = new Stack();
            string strResult = "";
            double a1, a2, result;
            int a3;
            string[] str = strExpression.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case "*":
                        a1 = Double.Parse(stack.Pop().ToString());
                        a2 = Double.Parse(stack.Pop().ToString());
                        result = a2 * a1;
                        stack.Push(result.ToString());
                        break;
                    case "/":
                        a1 = Double.Parse(stack.Pop().ToString());
                        a2 = Double.Parse(stack.Pop().ToString());
                        result = a2 / a1;
                        stack.Push(result.ToString());
                        break;
                    case "%":
                        a1 = Double.Parse(stack.Pop().ToString());
                        a2 = Double.Parse(stack.Pop().ToString());
                        result = a2 % a1;
                        stack.Push(result.ToString());
                        break;
                    case "^":
                        a1 = Double.Parse(stack.Pop().ToString());
                        a2 = Double.Parse(stack.Pop().ToString());
                        result = Math.Pow(a2, a1);
                        stack.Push(result.ToString());
                        break;
                    case "+":
                        a1 = Double.Parse(stack.Pop().ToString());
                        a2 = Double.Parse(stack.Pop().ToString());
                        result = a2 + a1;
                        stack.Push(result);
                        break;
                    case "-":
                        a1 = Double.Parse(stack.Pop().ToString());
                        a2 = Double.Parse(stack.Pop().ToString());
                        result = a2 - a1;
                        stack.Push(result);
                        break;
                    case "!":
                        a3 = int.Parse(stack.Pop().ToString());                      
                        result = a3;
                        if (result == 0) result = 1;
                        else for(int j = a3;j>1;j--)
                        {
                            result = result*(j-1);
                        }
                        str[i] = result.ToString();
                        stack.Push(Double.Parse(str[i]));
                        break;
                    case "√":
                        a1 = Double.Parse(stack.Pop().ToString());
                        result = Math.Sqrt(a1);
                        str[i] = result.ToString();
                        stack.Push(Double.Parse(str[i]));
                        break;
                    default:
                        stack.Push(Double.Parse(str[i]));
                        break;
                }
            }
            strResult = stack.Pop().ToString();
            return strResult;
        }
    }
}
