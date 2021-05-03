using System;
using System.Linq;

  //interface to abstract cipher/decipher tool
interface ICipher{
  string CipherName { get; }
  string Cipher(string srcText, string key);
  string Decipher(string cipherText, string key);
}

//method: rail fence
class RailFenceCipher : ICipher{
  public string CipherName { get { return "Метод железнодорожной изгороди"; } }
  public string Cipher(string srcText, string key = "3") {
    int i, j, period, intKey = Convert.ToInt32(key);
    string resText = "";
    period = 2 * (intKey - 1);
    for (i = 0; i < intKey; i++){
      for (j = 0; j < srcText.Length; j++){
        int remainder = j % period;
        if (i == intKey - 1 - Math.Abs(intKey - 1 - remainder)){
          resText += srcText.ElementAt(j);
        }
      }
    }
    return resText;
  }
  public string Decipher(string srcText, string key = "3") {
    int i, j, k, period, intKey = Convert.ToInt32(key);
    string resText = "";
    period = 2 * (intKey - 1);
    char[,] decipherArr = new char[intKey,srcText.Length];

    k = 0;
    for (i = 0; i < intKey; i++){
      for (j = 0; j < srcText.Length; j++){
        int remainder = j % period;
        if (i == intKey - 1 - Math.Abs(intKey - 1 - remainder)){
          decipherArr[i,j] = srcText.ElementAt(k++);
        }
      }
    }

    for (j = 0; j < srcText.Length; j++) {
      int remainder = j % period;
      i = intKey - 1 - Math.Abs(intKey - 1 - remainder);
      resText += decipherArr[i,j];
    }
    return resText; 
  }
}

//method: columns
class ColumnTransposCipher : ICipher {
  public string CipherName { get { return "Столбцовый метод"; } }
  public string Cipher(string srcText, string key = "sample") {
    key = key.ToLower();
    int width = key.Length;
    int height = (srcText.Length / width) + (srcText.Length % width == 0 ? 0 : 1);
    int i,j,k;
    string resText = "";
    char[,] cipherArr = new char[height, width];
    k = 0;
    for (i = 0; i < height; i++) { 
      for (j = 0; j < width; j++) {
        cipherArr[i, j] = k < srcText.Length ? srcText.ElementAt(k++) : ' ';
      }
    }
    for (char exdee = 'a';exdee <= 'z'; exdee++){
      for (j = 0; j < width; j++) {
        if (Convert.ToChar(key.ElementAt(j)) == exdee) {
          for (i = 0; i < height; i++) {
            resText += cipherArr[i, j];
          }
        }
      }
    }
    return resText;
  }

  public string Decipher(string srcText, string key = "sample") {
    key = key.ToLower();
    int width = key.Length;
    int height = (srcText.Length / width) + (srcText.Length % width == 0 ? 0 : 1);
    int i, j, k = 0;
    string resText = "";
    char[,] cipherArr = new char[height, width];
    for (char exdee = 'a'; exdee <= 'z'; exdee++) {
      for (j = 0; j < width; j++) {
        if (Convert.ToChar(key.ElementAt(j)) == exdee) {
          for (i = 0; i < height; i++) {
            cipherArr[i, j] = srcText.ElementAt(k++);
          }
        }
      }
    }

    for (i = 0; i < height; i++) {
      for (j = 0; j < width; j++) {
        resText += cipherArr[i, j];
      }
    }
    return resText.TrimEnd();
  }
}

class RotGridCipher : ICipher {
  public string CipherName { get { return "Метод поворачивающейся решетки"; } }
  private int[,] holes = new int[16, 2] { { 0, 0 }, { 1, 3 }, { 2, 2 }, { 3, 1 }, { 0, 3 }, { 1, 0 }, 
                                          { 2, 1 }, { 3, 2 }, { 0, 2 }, { 1, 1 }, { 2, 0 }, { 3, 3 }, 
                                          { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 0 } };
  public string Cipher(string srcText, string key = "none") {
    int i,j, k = 0;
    string resText = "";
    var cipherArr = new char[4,4];
    while (k < srcText.Length){
      for(i = 0; i < 16; i++){
        cipherArr[holes[i, 0], holes[i, 1]] = k < srcText.Length ? srcText.ElementAt(k++) : ' '; 
      }
      for(i = 0; i < 4; i++) {
        for (j = 0; j < 4; j++) {
          resText += cipherArr[i, j];
        }
      }
    }
    return resText; 
  }
  public string Decipher(string srcText, string key = "none") {
    int i, j, k = 0;
    string resText = "";
    var cipherArr = new char[4, 4];
    while (k < srcText.Length) {
      for (i = 0; i < 4; i++) {
        for (j = 0; j < 4; j++) {
          cipherArr[i, j] = k < srcText.Length ? srcText.ElementAt(k++) : ' ';
        }
      }

      for (i = 0; i < 16; i++) {
        resText += cipherArr[holes[i, 0], holes[i, 1]];
      }
    }
    return resText.TrimEnd();
  }
}

class CeasarCipher : ICipher {
  public string CipherName { get { return "Шифр Цезаря"; } }
  public string Cipher(string srcText, string key = "3") {
    int i,intKey = Convert.ToInt32(key);
    string resText = "";
    for (i = 0; i < srcText.Length; i++) {
      char bufChar = srcText.ElementAt(i);
      if (bufChar >= 'a' && bufChar <= 'z') {
        bufChar = Convert.ToChar(((bufChar - 'a') + intKey) % 26 + 'a');
      }else if(bufChar >= 'A' && bufChar <= 'Z') {
        bufChar = Convert.ToChar(((bufChar - 'A') + intKey) % 26 + 'A');
      }
      resText += bufChar;
    }
    return resText;
  }
  public string Decipher(string srcText, string key = "3") {
    int i, intKey = Convert.ToInt32(key);
    string resText = "";
    for (i = 0; i < srcText.Length; i++) {
      char bufChar = srcText.ElementAt(i);
      if (bufChar >= 'a' && bufChar <= 'z') {
        bufChar = Convert.ToChar(bufChar - intKey < 'a' ? bufChar - intKey + 26 : bufChar - intKey);
      }else if (bufChar >= 'A' && bufChar <= 'Z') {
        bufChar = Convert.ToChar(bufChar - intKey < 'A' ? bufChar - intKey + 26 : bufChar - intKey);
      }
      resText += bufChar;
    }
    return resText;
  }
}
