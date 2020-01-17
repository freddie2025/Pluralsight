package com.pluralsight.blog;

import com.pluralsight.blog.data.CategoryRepository;
import com.pluralsight.blog.data.PostRepository;
import com.pluralsight.blog.model.Category;
import com.pluralsight.blog.model.Post;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;

import java.util.List;
import java.util.Optional;

@Controller
public class BlogController {

    private PostRepository postRepository;

    public BlogController(PostRepository postRepository) {
        this.postRepository = postRepository;
    }

    @RequestMapping("/")
    public String listPosts(ModelMap modelMap) {
        List<Post> posts = postRepository.findAll();
        modelMap.put("posts", posts);
        return "home";
    }

    @RequestMapping("/post/{id}")
    public String postDetails(@PathVariable Long id, ModelMap modelMap) {
        Post post = postRepository.findById(id).orElse(null);
        modelMap.put("post", post);
        return "post-details";
    }
}
