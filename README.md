## Example usage
```c#
uint xor = 0x57676592;

// Unpack
var inFile = File.OpenRead("cabal.enc");
var outFile = File.Create("cabal.enc.xml");

Inflate(inFile, outFile, xor);

inFile.Close();
outFile.Flush();
outFile.Close();


// Pack
inFile = File.OpenRead("cabal.enc.xml");
outFile = File.Create("cabal_new.enc");

Deflate(inFile, outFile, xor, 9 /* Valid compression levels are from 0 to 9 */);

inFile.Close();
outFile.Flush();
outFile.Close();
```
