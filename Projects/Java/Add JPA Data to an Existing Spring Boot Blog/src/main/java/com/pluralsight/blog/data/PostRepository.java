package com.pluralsight.blog.data;

import com.pluralsight.blog.model.Post;
import org.springframework.stereotype.Component;

import java.util.*;

@Component
public class PostRepository {

    private final List<Post> ALL_POSTS = new ArrayList<>(Arrays.asList(
            new Post(1l, "Earbuds",
                    "You have got to try these in your ears. So tiny and can even block the sounds of screaming toddlers if you so desire.",
                    "You have got to try these in your ears. So tiny and can even block the sounds of screaming toddlers if you so desire.",
                    "Sarah Holderness", new Date("10/31/2019")),
            new Post(2l, "Smart Speakers",
                    "Smart speakers listen to you all right.  Sometimes they get a little snippy but will still order your favorite takeout.",
                    "Smart speakers listen to you all right.  Sometimes they get a little snippy but will still order your favorite takeout.",
                    "Sarah Holderness", new Date()),
            new Post(3l, "Device Charger",
                    "We all do a little too much scrolling in lieu of human interaction. This charger will keep you isolated.",
                    "We all do a little too much scrolling in lieu of human interaction. This charger will keep you isolated.",
                    "Sarah Holderness", new Date()),
            new Post(4l, "Smart Home Lock",
                    "Want to play tricks on your teenager? This smart home lock will lock them out when they act like they run the house.",
                    "Want to play tricks on your teenager? This smart home lock will lock them out when they act like they run the house.",
                    "Sarah Holderness", new Date("10/31/2019")),
            new Post(5l, "Smart Instant Pot",
                    "This Instant Pot can do your shopping for you. When it gets home it will also put your meal together.",
                    "This Instant Pot can do your shopping for you. When it gets home it will also put your meal together.",
                    "Sarah Holderness", new Date("10/31/2019")),
            new Post(6l, "Mobile Tripod",
                    "Best gift for that older adult in your life who cannot keep their face in the FaceTime window.",
                    "Best gift for that older adult in your life who cannot keep their face in the FaceTime window.",
                    "Sarah Holderness", new Date("10/31/2019")),
            new Post(7l, "Travel Keyboard",
                    "You never know when inspiration for your latest novel will strike. Meet the perfect travel keyboard for your random thoughts.",
                    "You never know when inspiration for your latest novel will strike. Meet the perfect travel keyboard for your random thoughts.",
                    "Sarah Holderness", new Date("10/31/2019"))
    ));

    public List<Post> findAll() {
        return ALL_POSTS;
    }

    public Optional<Post> findById(Long id) {
        for(Post post : ALL_POSTS) {
            if (post.getId() == id)
                return Optional.of(post);
        }
        return Optional.empty();
    }
}
