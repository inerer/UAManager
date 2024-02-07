using UAM.Core.EmailException;

while (true)
{
    await Mail.ReadEmails();

    await Task.Delay(1000);
}