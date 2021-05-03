using System;

class Program{
  static int Main(string[] args){
    bool isCorrect = false;
    bool mode = false;
    string usr_input = "";
    var railFenceCipher = new RailFenceCipher();
    var columnTransposCipher = new ColumnTransposCipher();
    var rotGridCipher = new RotGridCipher();
    var ceasarCipher = new CeasarCipher();
    ICipher cipher = rotGridCipher;
    string srcstring;
    Console.WriteLine("Выбор операции: ");
    Console.WriteLine("1 - Шифровать текст");
    Console.WriteLine("2 - Дешифровать текст");
    usr_input = Console.ReadLine();

    switch (usr_input){
      case "1":
        mode = true;
      break;
      case "2":
        mode = false;
      break;
      default:
        Console.WriteLine("Выход...");
      return 1;
    }
                            
    Console.Write("Введите текст: ");
    srcstring = Console.ReadLine();
    Console.WriteLine("Выберите метод: ");
    Console.WriteLine("1 - {0}",railFenceCipher.CipherName);
    Console.WriteLine("2 - {0}", columnTransposCipher.CipherName);
    Console.WriteLine("3 - {0}", rotGridCipher.CipherName);
    Console.WriteLine("4 - {0}", ceasarCipher.CipherName);
    usr_input = Console.ReadLine();
    switch (usr_input){
      case "1":
        cipher = railFenceCipher;
        Console.Write("Введите ключ (целое число от 2 до {0}): ",srcstring.Length - 1);
        usr_input = Console.ReadLine();
        if ( Convert.ToInt32(usr_input) > 1 && Convert.ToInt32(usr_input) < srcstring.Length) {
          isCorrect = true;
        }else {
          Console.WriteLine("Некорректное значение");}             
      break;
      case "2":
        cipher = columnTransposCipher;
        Console.Write("Введите ключ (маленькими латинскими буквами, например abcdef): ");
        usr_input = Console.ReadLine();
        if (usr_input.Length > 1){
          isCorrect = true;
        }else{
          Console.WriteLine("Некорректное значение");}          
      break;
      case "3":
        cipher = rotGridCipher;
        isCorrect = true;
      break;
      case "4":
        cipher = ceasarCipher;
        Console.Write("Введите ключ (целое число от 0 до {0}): ", 26);
        usr_input = Console.ReadLine();
        if (Convert.ToInt32(usr_input) >= 0 && Convert.ToInt32(usr_input) < 26) {
          isCorrect = true;
        }else {
          Console.WriteLine("Некорректное значение");}
      break;
    }

    if (isCorrect) {
      if (mode) {
        Console.WriteLine("Зашифрованный текст: |{0}|", cipher.Cipher(srcstring,usr_input));
      }else {
        Console.WriteLine("Дешифрованный текст: |{0}|", cipher.Decipher(srcstring, usr_input));
      }
    }
    return 0;
  }
}
