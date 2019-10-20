.assembly extern mscorlib {}
.assembly Hello {}
.module Hello.exe

.class Hello.Program
extends [mscorlib]System.Object
{
 .method static void Main(string[] args)
 cil managed
 {
  .entrypoint
  ldstr "{0}-{1}-{2}"
  ldstr "x"
  ldstr  "y"
  dup
  call void [mscorlib]System.Console::
  WriteLine(string, object, object, object)
  ret
 }
}