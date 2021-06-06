using UnityEngine;
using UnityEngine.UI;

public class Glyph : MonoBehaviour
{
    public GlyphType.List type;
    public Color color;
    public GameObject UI_glyph;

    private void Start() {        
        GetComponent<SpriteRenderer>().color = color;
        UI_glyph = GameObject.Find("HUD/hudGame/FOOTER/footerLeft/GlyphsPanel");
        setGlyphPanelColor();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("sword")){
            StageParameters.instance.glyphObtained.Add(this.type.ToString());
            UI_glyph.SetActive(true);
            UI_glyph.transform.Find("glyph"+type+"Icon").gameObject.SetActive(true);
            setGlyphPanelColor();
            Destroy(gameObject);
        }
    }

    private void setGlyphPanelColor(){
        int qtListGlyphs = System.Enum.GetNames(typeof(GlyphType.List)).Length;
        Color c = ItemColor.gray(); 

        if(StageParameters.instance.glyphObtained.Count >= qtListGlyphs){
            c = ItemColor.orange();            
        }else if(StageParameters.instance.glyphObtained.Count >= 4){
            c = ItemColor.purple();            
        }else if(StageParameters.instance.glyphObtained.Count >= 3){
            c = ItemColor.blue();            
        }else if(StageParameters.instance.glyphObtained.Count >= 2){
            c = ItemColor.green();            
        }

        UI_glyph.GetComponent<Image>().color = new Color(c.r,c.g,c.b,0.8f);
    }
}
