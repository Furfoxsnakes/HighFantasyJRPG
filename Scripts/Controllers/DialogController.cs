using Godot;
using System.Collections;
using System.Collections.Generic;
using HighFantasyJRPG.Scripts.Resources;
using Newtonsoft.Json;

public class DialogController : Node
{
    public static DialogController Instance => _instance;
    private static DialogController _instance;
    
    private const string _dialogDataFolderPath = "res://Resources/DialogData";

    private DialogBox _dialogBox;
    private DialogOptions _dialogOptions;
    private IEnumerator _dialog;
    private string _speakerName;
    private string _portraitPath;
    private NpcDialogData _npcDialogData;

    public override void _Ready()
    {
        if (_instance == null)
            _instance = this;
        else
            QueueFree();
        
        _dialogBox = GetNode<DialogBox>("DialogBox");
        _dialogOptions = GetNode<DialogOptions>("DialogOptions");

        //DisplayDialog("BlacksmithDialogData");
        //DisplayDialogOptions("BlacksmithDialogData");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
            if (mouseButton.Pressed)
                Next();
    }

    private IEnumerator Sequence(DialogData data)
    {
        var presenter = _dialogBox.Display(_speakerName,data);
        presenter.MoveNext();
        yield return null;
        
        while (presenter.MoveNext())
            yield return null;
        
        EndDialog();
    }

    public void Next()
    {
        if (_dialog == null) return;

        _dialog.MoveNext();
    }

    public void DisplayDialog(string path, int id)
    {
        _npcDialogData = loadDialogData(path);
        DisplayDialog(id);
    }

    public void DisplayDialog(int id)
    {
        if (_npcDialogData == null) return;

        _speakerName = _npcDialogData.Name;
        _portraitPath = _npcDialogData.PortraitPath;
        _dialog = Sequence(_npcDialogData.Dialog[id]);
        _dialog.MoveNext();
    }

    public void DisplayDialogOptions(string path)
    {
        _npcDialogData = loadDialogData(path);
        _dialogOptions.ShowOptions(_npcDialogData);
    }

    private void EndDialog()
    {
        _npcDialogData = null;
        _dialogBox.Visible = false;
    }

    private void _on_DialogOptions_item_activated(int id)
    {
        _dialogOptions.Visible = false;
        DisplayDialog(id);
    }
    
    private NpcDialogData loadDialogData(string path)
    {
        var file = new File();
        var error = file.Open($"{_dialogDataFolderPath}/{path}.json", File.ModeFlags.Read);
        
        if (error != Error.Ok)
        {
            GD.PrintErr("There was an error opening the file. Please try again.");
            return null;
        }
        
        var json = file.GetAsText();
        file.Close();
        
        return JsonConvert.DeserializeObject<NpcDialogData>(json);
    }

    private void SaveData(string name)
    {
        var npcData = new NpcDialogData()
        {
            Name = name,
            Dialog = new List<DialogData>()
            {
                new DialogData(){Name = "Quest title", Messages = new List<string>(){"This is some quest text"}}
            }
        };

        var json = JsonConvert.SerializeObject(npcData);

        var file = new File();
        file.Open($"{_dialogDataFolderPath}/{name}.json", File.ModeFlags.Write);
        file.StoreString(json);
        file.Close();
    }
}
