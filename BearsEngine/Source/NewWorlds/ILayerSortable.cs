namespace BearsEngine.Source.NewWorlds;

internal interface ILayerSortable
{
    float Layer { get; set; }

    event EventHandler<LayerChangedEventArgs>? LayerChanged;
}
