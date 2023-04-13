public class MaterialBehaviour : CharacterBehaviour
{
    public override void Init(CharacterProcessor processor)
    {
        base.Init(processor);
        processor.MeshRenderer.material = character.Data.CharacterMaterial;
    }
}