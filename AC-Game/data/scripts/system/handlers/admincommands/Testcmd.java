package admincommands;

import com.aionemu.gameserver.model.gameobjects.player.Player;
import com.aionemu.gameserver.utils.PacketSendUtility;
import com.aionemu.gameserver.utils.chathandlers.AdminCommand;

/**
 * Created by Kill3r
 */
public class Testcmd extends AdminCommand {

    public Testcmd() {
        super("testcmd");
    }

    public void execute(Player player, String...params){
        PacketSendUtility.sendMessage(player, "working..");
        if(params[0].equals("meow")){
            player.getEffectController().updatePlayerEffectIconsImpl();
            return;
        }
        if(params[0].equals("muh")){
            player.getGameStats().updateStatInfo();
            return;
        }
        if(params[0].equals("meh")){
            player.getGameStats().updateStatsAndSpeedVisually();
            return;
        }
        if(params[0].equals("moh")){
            player.getGameStats().updateStatsVisually();
        }
        player.getEffectController().updatePlayerEffectIcons();
        player.getEffectController().broadCastEffects();



        /*
        TIntObjectHashMap<ItemTemplate> it = DataManager.ITEM_DATA.getItemData();
        String allitemNames = "";
        int total = 0;

        for(ItemTemplate id : DataManager.ITEM_DATA.getItemData().valueCollection()){
            if(id.getUseLimits().getMinRank() == 18){
                if(allitemNames.length() > 23){
                    total = total + 1;
                    PacketSendUtility.sendMessage(player, allitemNames + id.getTemplateId() + ", ");
                    allitemNames = "";
                }else{
                    total = total + 1;
                    allitemNames += id.getTemplateId() + ", ";
                }

            }
        }
        PacketSendUtility.sendMessage(player, "Total Count of Items : " + total);
        */

    }
}
