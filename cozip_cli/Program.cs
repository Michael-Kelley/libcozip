using System.IO;
using System.Linq;
using System.Text;

namespace COZip.Tool {
	class Program {
		static void Main(string[] args) {
			uint xor = 0x57676592;
			var dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

			if (File.Exists($"{dir}/xors.cfg")) {
				var xors = File.ReadAllText($"{dir}/xors.cfg").Trim().Split(' ').Select(s => uint.Parse(s)).ToArray();
				xor = xors[0] | (xors[1] << 8) | (xors[2] << 16) | (xors[3] << 24);
			}

			foreach (var a in args) {
				var ext = Path.GetExtension(a);

				if (ext == ".enc")
					using (var inf = File.OpenRead(a)) {
						using (var ms = new MemoryStream()) {
							COZip.Inflate(inf, ms, xor);
							ms.Seek(0, SeekOrigin.Begin);
							var br = new BinaryReader(ms, Encoding.ASCII);

							while (char.IsWhiteSpace((char)br.PeekChar()))
								br.ReadChar();

							if (br.ReadChar() == '<')
								ext = ".xml";
							else
								ext = ".bin";

							ms.Seek(0, SeekOrigin.Begin);

							using (var outf = File.Create($"{Path.GetFileNameWithoutExtension(a)}{ext}"))
								ms.WriteTo(outf);
						}
					}
				else
					using (var inf = File.OpenRead(a)) {
						using (var outf = File.Create($"{Path.GetFileNameWithoutExtension(a)}.enc"))
							COZip.Deflate(inf, outf, xor);
					}
			}
		}
	}
}