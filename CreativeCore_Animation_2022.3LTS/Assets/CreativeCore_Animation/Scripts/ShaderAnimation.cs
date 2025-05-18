using UnityEngine;

public class ShaderAnimation : MonoBehaviour
{
    [Header("波浪动画")]
    public bool useWaveAnimation;
    public float waveSpeed = 1f;
    public float waveHeight = 0.1f;
    public float waveFrequency = 1f;
    
    [Header("溶解动画")]
    public bool useDissolveEffect;
    public float dissolveSpeed = 0.5f;
    public float dissolveMin = 0f;
    public float dissolveMax = 1f;
    
    private Material material;
    private float dissolveAmount;
    private bool dissolveDirection = true;

    void Start()
    {
        // 获取材质
        material = GetComponent<Renderer>().material;
        dissolveAmount = dissolveMin;
    }

    void Update()
    {
        if (useWaveAnimation)
        {
            // 更新波浪动画参数
            if (material.HasProperty("_WaveSpeed"))
            {
                material.SetFloat("_WaveSpeed", waveSpeed);
                material.SetFloat("_WaveHeight", waveHeight);
                material.SetFloat("_WaveFrequency", waveFrequency);
            }
        }
        
        if (useDissolveEffect)
        {
            // 更新溶解效果参数
            if (material.HasProperty("_DissolveAmount"))
            {
                // 溶解效果来回切换
                if (dissolveDirection)
                {
                    dissolveAmount += Time.deltaTime * dissolveSpeed;
                    if (dissolveAmount >= dissolveMax)
                    {
                        dissolveAmount = dissolveMax;
                        dissolveDirection = false;
                    }
                }
                else
                {
                    dissolveAmount -= Time.deltaTime * dissolveSpeed;
                    if (dissolveAmount <= dissolveMin)
                    {
                        dissolveAmount = dissolveMin;
                        dissolveDirection = true;
                    }
                }
                
                material.SetFloat("_DissolveAmount", dissolveAmount);
            }
        }
    }
}
