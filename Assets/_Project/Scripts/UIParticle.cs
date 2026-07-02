using UnityEngine;
using UnityEngine.UI;

public class UIParticle : MaskableGraphic
{
    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles;
    private UIVertex[] _quad = new UIVertex[4];
    private int _maxParticles = 0;

    [SerializeField] private Material _material;

    public override Texture mainTexture => _material.mainTexture ?? base.mainTexture;

    protected override void Awake()
    {
        base.Awake();
        _particleSystem = GetComponent<ParticleSystem>();
        _maxParticles = _particleSystem.main.maxParticles;
        _particles = new ParticleSystem.Particle[_maxParticles];

        var renderer = GetComponent<ParticleSystemRenderer>();
        if (renderer) renderer.enabled = false;
    }

    private void Update()
    {
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (!gameObject.activeInHierarchy) return;

        int count = _particleSystem.GetParticles(_particles);

        for (int i = 0; i < count; i++)
        {
            var particle = _particles[i];

            Vector2 pos = _particleSystem.main.simulationSpace == ParticleSystemSimulationSpace.Local
                ? particle.position
                : transform.InverseTransformPoint(particle.position);

            float size = particle.GetCurrentSize(_particleSystem) * 0.5f;

            Color32 color = particle.GetCurrentColor(_particleSystem);

            _quad[0] = UIVertex.simpleVert;
            _quad[0].position = new Vector3(pos.x - size, pos.y - size, 0);
            _quad[0].color = color;
            _quad[0].uv0 = new Vector2(0, 0);

            _quad[1] = UIVertex.simpleVert;
            _quad[1].position = new Vector3(pos.x - size, pos.y + size, 0);
            _quad[1].color = color;
            _quad[1].uv0 = new Vector2(0, 1);

            _quad[2] = UIVertex.simpleVert;
            _quad[2].position = new Vector3(pos.x + size, pos.y + size, 0);
            _quad[2].color = color;
            _quad[2].uv0 = new Vector2(1, 1);

            _quad[3] = UIVertex.simpleVert;
            _quad[3].position = new Vector3(pos.x + size, pos.y - size, 0);
            _quad[3].color = color;
            _quad[3].uv0 = new Vector2(1, 0);

            vh.AddUIVertexQuad(_quad);
        }
    }
}