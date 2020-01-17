package com.pluralsight.blog.data;

import com.pluralsight.blog.model.Category;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Component;

import java.util.List;
import java.util.Optional;


@Component
public class CategoryRepository {

    public List<Category> findAll() {
        return null;
    }

    public Optional<Category> findById(Long id) {
        return null;
    }
}
