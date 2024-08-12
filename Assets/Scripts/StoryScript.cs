
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class StoryScript : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public Dictionary<int, string> dict = new Dictionary<int, string>()
    {
        {1, "Today, we're entering a mysterious cave that's been the stuff of legends. It's believed to hide the 'Tomb of Eternal Vitality' a place that can give you eternal life. To find it, we need a special golden key. We're experienced adventurers, so we've come prepared. The cave entrance looks scary, but we're determined to uncover its secrets and, if we're lucky, claim the treasure that so many have sought but never found. Legend says that finding the golden key is the only way to open the tomb's doors and reveal its secrets. -Nana" },
        {2, "Our journey through this labyrinthine cave took an unexpected turn today.\r\n\r\nAs we wandered through the twisting tunnels, we stumbled upon a grim discovery - the bones of a fellow explorer, long since deceased. Beside the skeletal remains, we found a note, a desperate message from someone who met their end in this desolate place.\r\n\r\nThe note reads:\r\n\"My brother told me to stay put, and he went deeper into the search for a key he believes is crucial for opening the door that leads to the golden key. The key he's looking for is vital for opening a door, and beyond that door, there's another item that's necessary to acquire the golden key, but it's all based on our grandfather's guess without absolute certainty.\"      -Nana" },
        {3, "In the cold, unforgiving heart of this relentless cave, I've finally found the key. But at what terrible cost...\r\n\r\nThe weight of guilt bears down on me like an unforgiving boulder. It was my insatiable greed, my burning desire to uncover the treasures hidden within this cursed labyrinth, that drove us deeper into this abyss. I wanted to explore, to find the key that would unlock untold riches, and I convinced my small brother to join me. Now, he's waiting somewhere in this labyrinthine darkness.\r\n\r\nPanic grips my soul, and frustration turns to self-loathing. If I don't find my way out soon, I pray to God that my brother has the wisdom to abandon this cursed quest and return to the surface without searching for me. \r\n\r\n I must reunite with him, for he is my guiding light in this suffocating darkness, and I refuse to let this forsaken cave claim both of us." },
        {4, "As we descended further into the cave's enigmatic depths, the air grew colder, and the labyrinthine tunnels became increasingly complex. Remarkably, we've stumbled upon a chamber adorned with intricate glowing runes etched into the stone. We're carefully studying these runes, considering their potential use in the future.\r\n\r\nWe also noticed a door, much like the one we encountered a day ago near the body and the ominous note. Just like before, this door remains locked, its secrets concealed behind ancient mechanisms. It's clear that these doors require a key to unlock, and the whereabouts of these keys remain a mystery shrouded in darkness.\r\n\r\nHowever, Atch, our resolute leader, has made a decision. He ordered some of the team members to go and search for the key. While me and remaining started \r\nstudying the glowing runes, documenting the findings and meticulously drawing maps of the passages we've explored.     -Nana"},
        {5, "As hours turned into a full day, worry began to gnaw at us. Atch's decision to send the group ahead, in search of the key that might unlock our path, now weighs heavy on his shoulders. The eerie whispering in the cave has grown more pronounced, as if it carries a sinister message.\r\n\r\nThough Atch maintains his determination, the uncertainty of our situation is undeniable. We can only hope that our fellow explorers will return soon, shedding light on the mysteries of this relentless cave. Our hopes are pinned on their success, both in finding the key and unraveling the secrets hidden within these ancient walls. With bated breath, we await their return.     -Nana"},
        {6, "Two agonizing days have passed, and the group sent by Atch has not returned. We are gripped by worry as we pack up our belongings and embark on a desperate search for the missing members.\r\n\r\nIn the heart of this oppressive darkness, we discovered one of our group members, traumatized and speechless. His vacant eyes bore witness to horrors he could not put into words, and he could only tremble, unable to answer our frantic inquiries about the fate of the other two who had ventured with him.\r\n\r\nThe group's division is now clear: some, driven by fear, have chosen to turn back and escape this ominous cave . Atch, ever resolute, leads the way deeper into the cave, and I've made the choice to stand by his side, searching for our missing companions.       -Nana" },
        {7, "It appears that we've discovered the chamber containing the Golden Key, which is the means to open the secret door leading to Eternal Vitality. It seems that a key is required to unlock it.      -WittyWalter and GigglingGina"},
        {8, "I've at last arrived at the doors that guard the Book containing the secrets of eternal vitality. It's almost unbelievable that the legends were actually true. I wish my team hadn't left me here, but I can't blame them, considering what we've been through. Strangely, there's another door nearby. Who knows what lies behind it?        -Atch" },
        {9, "If you find this letter, it means it's your last chance to escape from this cave. The stories about a special key and a magical book are probably not true. Atch and I got separated while looking for them, and after searching for a long time, we couldn't find anything. I hope Atch made it out of the cave. You should leave and get back to the surface while you still can!    -Anna" },
        {10, "This blue crystal can be used for money." },
        {11, "Looks like a king or queen's crown that can be used for money or research" },
        {12, "A eerie crown with the appearance of having been utilized by a cult or an ancient civilization, and it can be used for either money or research." },
        {13, "An old key for opening a gate" },
        {14, "An old key for opening a gate" },
        {15, "An old key for opening a gate" },
        {16, "A golden skull, its history and the ancient method of its creation remain uncertain. It can be used for money." },
        {20, "This is a golden key, and it is thought to have the ability to unlock the gates that guard the Legendary Book \"Eternal Vitality.\"" },
        {21, "Gold Coins can be used as money" },
        {22, "Gold Coins can be used as money" },
        {23, "Gold Coins can be used as money" },
        {24, "Gold Coins can be used as money" },
        {26, "Pickaxe can be used to mine crystals" },
        {30, "This book, known as \"Eternal Vitality,\" is said to have been concealed within the depths of this cave. Numerous legends suggest it possesses the secrets to eternal youth, and with dedicated research, we might uncover its hidden knowledge and potentially unlock the mystery of everlasting vitality, bridging the realms of reality and mystery." }
    };


}
