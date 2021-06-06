using UnityEngine;

public abstract class PnjStat : MonoBehaviour, PnjManagement
    {
    public int level = 0;
    private int talked = 0;
    public PopUpMessageUnlock[] popUps;

    public void giveHeart()
    {
        if(popUps.Length>level){
            popUps[level].displayPopUp();
        }else{
            Debug.LogWarning("Il n'y a pas de popupmessage pour le debloquage du reward level "+(level+1));
        }
        level++;
    }
    public int getLevel()
    {
        return level;
    }
    public void talk()
    {        
        if(level > talked){
            talked++;
        }       
    }
    public int getTalked()
    {
        return talked;
    }

    public void loadStats(int[] stats){
        level = stats[0];
        talked = stats[1];
    }
    public int[] saveStats(){
        return new int[] {level,talked};
    }
}
