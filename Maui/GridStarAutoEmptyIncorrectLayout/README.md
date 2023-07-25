# Grid with a star and an empty auto column leads to an incorrect layout

This sample project demonstrates a bug in .NET MAUI when using a `Grid` with a `*` and `Auto` columns. When the `Auto` column has no content, the `Grid` is rendered with an incorrect layout. The status of the bug can be tracked from the following [.NET MAUI Bug Report](https://github.com/dotnet/maui/issues/16334).