using Godot;
using System;
using System.Collections;
using System.Linq;

public class DialogBox : Control
{
    private Label _speakerName;
    private RichTextLabel _text;
    private Control _indicator;
    
    public override void _Ready()
    {
        _speakerName = GetNode<Label>("SpeakerName");
        _text = GetNode<RichTextLabel>("Text");
        _indicator = GetNode<Control>("Indicator");
    }

    public IEnumerator Display(string speakerName, DialogData data)
    {
        Visible = true;
        _speakerName.Text = speakerName;

        for (var i = 0; i < data.Messages.Count; ++i)
        {
            _text.BbcodeText = data.Messages[i];
            _indicator.Visible = i + 1 < data.Messages.Count;
            yield return null;
        }
    }
}
