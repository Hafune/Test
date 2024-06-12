using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

public class ItemsStackClient : MonoConstruct
{
    [SerializeField] private ConvertToEntity _convertToEntity;

    private Context _context;

    protected override void Construct(Context context) => _context = context;

    public void Update(MyList<int> items)
    {
        
    }
}