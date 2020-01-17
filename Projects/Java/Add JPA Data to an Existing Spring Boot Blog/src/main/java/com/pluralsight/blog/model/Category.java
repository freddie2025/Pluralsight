package com.pluralsight.blog.model;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;


public class Category {

    private Long id;
    private String name;

    public Category() {
        super();
    }

    public Long getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Post> getPosts() {
        return null;
    }

    public void addPost(Post post) {
        return;
    }
}
