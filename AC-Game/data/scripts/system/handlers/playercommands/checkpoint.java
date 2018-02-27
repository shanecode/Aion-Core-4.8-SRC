package playercommands;

import com.aionemu.gameserver.model.gameobjects.player.Player;
import com.aionemu.gameserver.services.teleport.TeleportService2;
import com.aionemu.gameserver.utils.PacketSendUtility;
import com.aionemu.gameserver.utils.chathandlers.PlayerCommand;


/**
 * Created by Kill3r
 */
public class checkpoint extends PlayerCommand {
    public checkpoint(){
        super("checkpoint");
    }
    public void execute(Player player, String...params){
        if(player.getWorldId() != 220050000){
            PacketSendUtility.sendMessage(player, "You can only use this command inside the Jumper Event!");
            return;
        }

        int Checks = 0;
        Checks = player.getCheckpoints();
        if(Checks == 1){
            PacketSendUtility.announceMeOnly(player, "Teleporting to Checkpoint #1");
            TeleportService2.teleportTo(player, 220050000, 481.05518f,652.2917f,580.90686f);
        }else if(Checks == 2){
            PacketSendUtility.announceMeOnly(player, "Teleporting to Checkpoint #2");
            TeleportService2.teleportTo(player, 220050000, 551.31903f,383.68204f,287.51486f);
        }else if(Checks == 3){
            PacketSendUtility.announceMeOnly(player, "Teleporting to Checkpoint #3");
            TeleportService2.teleportTo(player, 220050000, 559.04333f,402.513f,304.62128f);
        }else if(Checks == 4){
            PacketSendUtility.announceMeOnly(player, "Teleporting to Checkpoint #4");
            TeleportService2.teleportTo(player, 220050000, 714.372f,565.5417f,270.0317f);
        }else if(Checks == 5){
            PacketSendUtility.announceMeOnly(player, "Teleporting to Checkpoint #5");
            TeleportService2.teleportTo(player, 220050000, 756.0162f,618.1168f,286.27634f);
        }else if(Checks == 6){
            PacketSendUtility.announceMeOnly(player, "Teleporting to Checkpoint #6");
            TeleportService2.teleportTo(player, 220050000, 779.28235f,638.30145f,287.08456f);
        }else{
            TeleportService2.teleportTo(player, 220050000, 468.792f, 423.079f, 233.494f);
            PacketSendUtility.announceMeOnly(player, "You havn't acquired any Checkpoints yet!");

        }
    }
}
