using UnityEngine;

public class WindController : MonoBehaviour
{
   [SerializeField] private float windStrength;
   [SerializeField] private float windSpeed;
   [SerializeField] private float windFrequency;
   
   private SpriteRenderer _spriteRenderer;
   private Material _windMaterial;
   
   private static readonly int WindStrength = Shader.PropertyToID("_WindStrength");
   private static readonly int WindSpeed = Shader.PropertyToID("_WindSpeed");
   private static readonly int WindFrequency = Shader.PropertyToID("_WindFrequency");

   void Awake()
   {
       _spriteRenderer = GetComponent<SpriteRenderer>();
       _windMaterial = _spriteRenderer.material;
   }

   private void Start()
   {
       ApplyWind();
   }

   private void OnValidate() //needs to call when the script is loaded i think... 
   {
       if (_spriteRenderer == null)
       {
           _spriteRenderer = GetComponent<SpriteRenderer>();
       }

       if (_spriteRenderer != null)
       {
           ApplyWind();
       }

       if (_spriteRenderer != null)
       {
           _windMaterial = _spriteRenderer.sharedMaterial;
           ApplyWind();
       }
       
   }

   private void ApplyWind() // to create the illusion of wind
   {
       if (_spriteRenderer == null)
       {
           return;
       }

       if (_windMaterial == null)
       {
           _windMaterial = _spriteRenderer.material;
       }

       if (_windMaterial == null)
       {
           return;
       }

       _windMaterial.SetFloat(WindStrength, windStrength);
       _windMaterial.SetFloat(WindSpeed, windSpeed);
       _windMaterial.SetFloat(WindFrequency, windFrequency);
   }
   
}
