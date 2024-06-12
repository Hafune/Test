using Core.Services;
using Lib;
using Reflex;
using UnityEngine;

public class GlobalTeleportInstance : MonoConstruct
{
    private Context _context;
    private GlobalTeleportService _service;

    protected override void Construct(Context context) => _context = context;

    private void Awake() => _service = _context.Resolve<GlobalTeleportService>();

    private void OnTriggerEnter2D(Collider2D col) => _service.InTouch(transform.position);

    private void OnTriggerExit2D(Collider2D other) => _service.OutOfTouch();
}