using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SangueSugaHabilidade : MonoBehaviour
{   
    [Tooltip(" Tempo em segundos para o objeto ser destruido")]
    public float Tempo_Destruir;
    [Tooltip(" Velocidade que objeto vai se mover")]
    public float Velocidade;
    [Tooltip("Referencia do jogado que usou a skil")]
    public GameObject Origem;
    [Tooltip(" Dano da SKill  !!! LEMBRANDO QUE ESSE VARIAL RECEBE O VALOR EM ATRIBUTOS NA PARTE DE INSTANCIAR !!!")]
    public float Dano_Da_Skill;
    [Tooltip("Poder da skil usada pelo jogador")]
    public float Poder_Da_Skil;

    private Rigidbody Corpo;

    void Start()
    {
        Corpo = GetComponent<Rigidbody>();

        StartCoroutine(Destruir(Tempo_Destruir));
    }
    void Update()
    {
        Corpo.velocity = transform.forward * Velocidade;
    }
    public IEnumerator Destruir(float _Tempo_Destruir)
    {
        yield return new WaitForSeconds(_Tempo_Destruir);
        Destroy(this.gameObject);
    }

        void OnTriggerEnter(Collider Enconstei)
    {
        if(Enconstei.gameObject.CompareTag("Inimigo"))
        {
            float Vida_Inicial;
            Vida_Inicial = Enconstei.GetComponent<Inimigo_Status>().Vida;
            Enconstei.GetComponent<Inimigo_Status>().Vida -=Dano_Da_Skill;
            Origem.GetComponent<ClasseNecromante>().atributos.vida +=Calcular_Cura(Enconstei.GetComponent<Inimigo_Status>().Vida,Vida_Inicial);
            Destroy(this.gameObject);
        }
    }

    public float Calcular_Cura(float _Vida_Atual , float _Vida_Inicial)
    {
        float Margem_De_Cura = _Vida_Inicial-_Vida_Atual;
        return (Margem_De_Cura*(Poder_Da_Skil*10))/100;
    }
}
