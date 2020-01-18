﻿// <Snippet1>
// To work correctly with the .aspx file for this sample, name the compiled DLL 
// generated from this class ctrlBuilder_1.dll.
// Create a namespace that contains two classes that inherit from the
// ControlBuilder class, MyItemControlBuilder and NoWhiteSpaceControlBuilder.
using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;

namespace CustomControlBuilders
{

  // <Snippet2>
  // Create a custom ControlBuilder that interprets nested elements
  // named myitem as TextBoxes. If this class is called in a 
  // ControlBuilderAttribute applied to a custom control, when
  // that control is created in a page and it contains child elements
  // that are named myitem, the child elements will be rendered as 
  // TextBox server controls. This control builder also ignores literal
  // strings between elements when it is associated with a control.
  [AspNetHostingPermission(SecurityAction.Demand, 
     Level=AspNetHostingPermissionLevel.Minimal)]
  public sealed class MyItemControlBuilder : ControlBuilder 
  {
     // Override the GetChildControlType method to detect
     // child elements named myitem.
     public override Type GetChildControlType(String tagName,
                                       IDictionary attributes)
     {
        if (String.Compare(tagName, "myitem", true) == 0) 
        {
           return typeof(TextBox);
        }
        return null;
     }

  // <Snippet3>
     // Override the AppendLiteralString method so that literal
     // text between rows of controls are ignored.  
     public override void AppendLiteralString(string s) 
     {
       // Ignores literals between rows.
     }
  // </Snippet3>
  }
  // </Snippet2>
  
  // <Snippet4>
  // Create a class that does not allow white space generated by a control
  // to be created as a LiteralControl.   
  [AspNetHostingPermission(SecurityAction.Demand, 
     Level=AspNetHostingPermissionLevel.Minimal)]
  public sealed class NoWhiteSpaceControlBuilder : ControlBuilder 
  {  
       public override bool AllowWhitespaceLiterals() 
       {
          return false;
       } 
  }
  // </Snippet4>

  // <snippet5>
  // Using the NowWhiteSpaceControlBuilder with a simple control.
  // When created on a page this control will not allow white space
  // to be converted into a literal control.     
  [ControlBuilderAttribute(typeof(NoWhiteSpaceControlBuilder))]
  [AspNetHostingPermission(SecurityAction.Demand, 
     Level=AspNetHostingPermissionLevel.Minimal)]
  public sealed class MyNonWhiteSpaceControl : Control
  {}

  // A simple custom control to compare with MyNonWhiteSpaceControl.
  [AspNetHostingPermission(SecurityAction.Demand, 
     Level=AspNetHostingPermissionLevel.Minimal)]
  public sealed class WhiteSpaceControl : Control 
  {}
  // </snippet5>   

  // <Snippet6>
  // Create a control that is modified by the MyItemControlBuilder
  // class. 
  [ControlBuilderAttribute(typeof(MyItemControlBuilder))] 
  [AspNetHostingPermission(SecurityAction.Demand, 
     Level=AspNetHostingPermissionLevel.Minimal)]
  public sealed class MyControl1 : Control
  {
    // Create an ArrayList object to store anl the controls
    // specified as nested elements within this control.
    private ArrayList items = new ArrayList();
   
    // This function is internally invoked by
    // IParserAccessor.AddParsedSubObject(Object).
    // When called, it adds any TextBox controls to the items
    // ArrayList object.
    protected override void AddParsedSubObject(Object obj) 
    {
       if (obj is TextBox) 
       {
          items.Add(obj);
       }
    }

    // Override the CreateChildControls to create any TextBox server control
    // contained within this control as child controls. 
    protected override void CreateChildControls()
    {
       System.Collections.IEnumerator myEnumerator = items.GetEnumerator();
       while(myEnumerator.MoveNext())
           this.Controls.Add((TextBox)myEnumerator.Current);
    } 
  }
  // </Snippet6>
}
// </snippet1>
