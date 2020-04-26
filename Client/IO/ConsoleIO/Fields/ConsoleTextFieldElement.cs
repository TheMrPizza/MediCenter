namespace Client.IO.ConsoleIO
{
    public class ConsoleTextFieldElement : ConsoleFieldElement<string>
    {
        protected override string ValidateInput()
        {
            return _input;
        }
    }
}
