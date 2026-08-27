# Virtualized Recycling ScrollView
The bread and butter of a virtualized recycling scrollview in about 300 lines of code:
https://github.com/telerik/ms-samples/pull/80

It steps on the ideas of this:
https://wwdcnotes.com/documentation/wwdcnotes/wwdc11-104-advanced-scrollview-techniques/

The scrollview will update the children within its content using a custom layout, it involves arranging items that disappear from the top to move them to the bottom also updating their content.

There are two number fields at the titlebar:
 - the left one shows measure and arrange counts for views outside the scrollview
 - the right one shows measure and arrange counts for views in the scrollview
