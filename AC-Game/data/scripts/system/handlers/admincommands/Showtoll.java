package admincommands;

import com.aionemu.gameserver.model.gameobjects.VisibleObject;
import com.aionemu.gameserver.model.gameobjects.player.Player;
import com.aionemu.gameserver.utils.PacketSendUtility;
import com.aionemu.gameserver.utils.chathandlers.AdminCommand;

/**
 * Created by Kill3r
 */
public class Showtoll extends AdminCommand {
    public  Showtoll() {
        super("showtoll");
    }


    public void execute(Player admin, String... params){


        VisibleObject visibleObject = admin.getTarget();


        if (visibleObject == null || !(visibleObject instanceof Player)){
            PacketSendUtility.sendMessage(admin, "Wrong Target Selected");
            return;
        }

        Player target = (Player) admin.getTarget();

        long TargetedTolls;

        TargetedTolls = target.getClientConnection().getAccount().getToll();

        PacketSendUtility.sendWhiteMessage(admin, target.getName() +" has " + TargetedTolls + " tolls in account '"+ target.getClientConnection().getAccount().getName() + "'");


        }


    public void onFail(Player player, String msg){
        PacketSendUtility.sendMessage(player, "You can only use it on Players");

    }
}
