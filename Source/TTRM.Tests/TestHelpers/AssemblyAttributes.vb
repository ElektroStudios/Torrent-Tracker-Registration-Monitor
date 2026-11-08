#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

' Tests in this assembly share process-wide state (current UI culture, the application cache folders,
' and the lazily-initialized static Roslyn references of DynamicPlugin), so they must not run in parallel.
<Assembly: DoNotParallelize>
