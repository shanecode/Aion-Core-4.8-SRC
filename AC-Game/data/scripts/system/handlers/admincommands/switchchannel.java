package admincommands;

import com.aionemu.gameserver.model.gameobjects.player.Player;
import com.aionemu.gameserver.services.teleport.TeleportService2;
import com.aionemu.gameserver.utils.chathandlers.AdminCommand;

/**
 * Created by Kill3r
 */
public class switchchannel extends AdminCommand {
    public switchchannel(){
        super("switchchannel");
    }
    public void execute(Player player, String...params){
        int Channel;
        Channel = Integer.parseInt(params[0]);
        TeleportService2.changeChannel(player, Channel);
    }
}
