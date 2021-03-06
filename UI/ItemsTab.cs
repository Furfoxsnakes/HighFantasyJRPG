using System.Linq;
using Godot;

public class ItemsTab : Tabs
{
    private PopupMenu _activateItemPopup;
    private Game _game;

    private Inventory _inventory;
    private ItemDetails _itemDetails;
    private ItemsList _itemsList;
    private Item _selectedItem;
    private int _selectedItemId;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _game = GetTree().Root.GetNode<Game>("Game");

        _activateItemPopup = Owner.GetNode<PopupMenu>("Container/ActivateItemPopup");
        _itemsList = GetNode<ItemsList>("HBoxContainer/ItemList");
        _itemDetails = GetNode<ItemDetails>("HBoxContainer/ItemDescriptionPanel/ItemDetails");

        _inventory = _game.Inventory;
        _itemsList.Connect("item_selected", this, nameof(OnItemSelected));
        _itemsList.PopulateItems(_selectedItemId);
    }

    private Item GetSelectedItem()
    {
        return _inventory.Items.ElementAt(_selectedItemId).Key;
    }

    private void OnItemSelected(int selectedId)
    {
        _selectedItemId = selectedId;
        _selectedItem = GetSelectedItem();
        _itemDetails.DisplayItemDetails(_selectedItem);
    }

    private void _on_ActivateItemButton_pressed()
    {
        if (_selectedItem == null) return;

        if (_activateItemPopup.Visible)
            _activateItemPopup.Hide();
        else
            _activateItemPopup.Show();

        _activateItemPopup.Clear();

        foreach (var partyMember in _game.PartyController.Party) _activateItemPopup.AddItem(partyMember.Name);
    }

    private void _on_ActivateItemPopup_index_pressed(int index)
    {
        var character = _game.PartyController.Party[index];
        ActivateSelectedItem(character);
    }

    private void ActivateSelectedItem(CharacterDetails character)
    {
        if (_selectedItem.Activate(character))
        {
            GD.Print($"{_selectedItem.Name} used successfully on {character.Name}");

            _inventory.Items[_selectedItem]--;
            if (_inventory.Items[_selectedItem] == 0)
            {
                _inventory.Items.Remove(_selectedItem);
                _selectedItem = null;
                _selectedItemId = _selectedItemId == 0 ? 0 : _selectedItemId - 1;
            }

            _itemsList.PopulateItems(_selectedItemId);
        }
        else
        {
            GD.Print($"{_selectedItem.Name} could not be used on {character.Name}");
        }
    }
}