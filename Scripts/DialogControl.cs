using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class DialogControl : MonoBehaviour
{
    [SerializeField] private GameObject Dialog;
    private TextMeshProUGUI TMproDialog;
    private DialogShow DialogShow;
    private Animator anim = null;
    private Image myimage;
    [SerializeField] Collider2D imageCollider;
    [SerializeField] LayerMask Player;
    [SerializeField] Vector2 size;
    private void Start() 
    {
        TMproDialog =   Dialog.GetComponent<TextMeshProUGUI>();
        DialogShow = Dialog.GetComponent<DialogShow>();
        anim = GetComponent<Animator>();
        StartCoroutine(DialogGo());
        myimage = GetComponent<Image>();
        Collider2D col = Physics2D.OverlapBox(gameObject.transform.position, size, 0f, Player);
    }

    private IEnumerator DialogGo(){
    //1秒待つ
    yield return new WaitForSeconds(1.0f);

    //1秒待った後の処理
    DialogStart();
    }

    private void Update() 
    {
       // 重なっているColliderを取得
        Collider2D[] overlappingColliders = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(Player);
        int overlapCount = imageCollider.OverlapCollider(filter, overlappingColliders);

        // Playerと重なっているかどうかを判定
        if (overlapCount > 0)
        {
            SetTransparency(0.3f);  // 半透明
        }
        else
        {
            SetTransparency(1f);  // 元に戻す
        }  
    }

    private void SetTransparency(float alpha)
    {
        // Imageの透明度を変更
            Color imageColor = myimage.color;
            imageColor.a = alpha;
            myimage.color = imageColor;

            Color textColor = TMproDialog.color;
            textColor.a = alpha;
            TMproDialog.color = textColor;
    }

    private void DialogPlay()
    {
        DialogShow.Show();
    }

    private void DialogStart()
    {
        anim.SetTrigger("DialogStart");
    }

        private void DialogEnd()
    {
        anim.SetTrigger("DialogEnd");
    }

    /*private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == PlayerTag)
        {
            myimage.color =  new Color(255, 255 ,255 ,60);
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == PlayerTag)
        {
            myimage.color =  new Color(255, 255 ,255 ,255);
            
        }
    }*/
}
