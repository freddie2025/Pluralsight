package com.pluralsight.module3;
import com.pluralsight.*;

import static org.junit.Assert.*;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;

import org.junit.BeforeClass;
import org.junit.Test;
import org.junit.Before;
import org.mockito.Mockito;
import org.junit.runner.RunWith;
import org.powermock.api.mockito.PowerMockito;
import org.powermock.core.classloader.annotations.PrepareForTest;
import org.powermock.modules.junit4.PowerMockRunner;

import java.lang.reflect.Method;

import java.io.*;


@RunWith(PowerMockRunner.class)
@PrepareForTest({DriverManager.class, PreparedStatement.class, BookDAO.class})
public class Module3_Task1_thru_5_IT {

    static Method method = null;
    static String sql = "UPDATE book SET title = ?, author = ?, price = ?" +
               " WHERE id = ?";
    Connection spyConnection;
    PreparedStatement mockStatement;
    static BookDAO bookDAO;
    static BookDAO spyBookDAO;
    static boolean called_prepareStatement = false;
    static boolean called_setTitle = false;
    static boolean called_setAuthor = false;
    static boolean called_setPrice = false;
    static boolean called_setId = false;
    static boolean called_executeUpdate = false;
    static boolean called_close = false;
    static String message = "";
    @Before
    public void setUp() {
      spyConnection = Mockito.mock(Connection.class);
      mockStatement = Mockito.mock(PreparedStatement.class);
      bookDAO = new BookDAO(spyConnection);
      spyBookDAO = Mockito.spy(bookDAO);

      Book tempBookObject = new Book(1, "1984", "George Orwell", 1.50f);
      try {
         Mockito.when(spyConnection.prepareStatement(sql)).thenReturn(mockStatement);
         method =  BookDAO.class.getMethod("updateBook", Book.class);
         method.invoke(spyBookDAO, tempBookObject);
      } catch (Exception e) {
         //e.printStackTrace();
      }
    }

    // Verify updateBook() method exists in BookDAO
    @Test
    public void _task1() throws Exception {
      message = "The method updateBook() doesn't exist in BookDAO.java.";
      assertNotNull(message, method);
    }

    @Test
    public void _task2() throws Exception {
      try {
        Mockito.verify(spyConnection).prepareStatement(sql);
        called_prepareStatement = true;
      } catch (Throwable e) {}

      message = "The method updateBook() doesn't call prepareStatement() correctly.";
      assertTrue(message, called_prepareStatement);
    }

    @Test
    public void _task3() throws Exception {
      try {
        Mockito.verify(mockStatement).setString(1, "1984");
        called_setTitle = true;
        Mockito.verify(mockStatement).setString(2, "George Orwell");
        called_setAuthor = true;
      } catch (Throwable e) {}

      message = "The method updateBook() doesn't call setString() for the title.";
      assertTrue(message, called_setTitle);

      message = "The method updateBook() doesn't call setString() for the author.";
      assertTrue(message, called_setAuthor);
    }

    @Test
    public void _task4() throws Exception {
      try {
        Mockito.verify(mockStatement).setFloat(3, 1.50f);
        called_setPrice = true;
        Mockito.verify(mockStatement).setInt(4, 1);
        called_setId = true;
      } catch (Throwable e) {}

      message = "The method updateBook() doesn't call setFloat() for the price.";
      assertTrue(message, called_setPrice);

      message = "The method updateBook() doesn't call setInt() for the id.";
      assertTrue(message, called_setId);
    }

    @Test
    public void _task5() throws Exception {
      try {
        Mockito.verify(mockStatement).executeUpdate();
        called_executeUpdate = true;
        Mockito.verify(mockStatement).close();
        called_close = true;
      } catch (Throwable e) {}

      message = "The method updateBook() doesn't call executeUpdate().";
      assertTrue(message, called_executeUpdate);

      message = "The method updateBook() doesn't call PreparedStatement close().";
      assertTrue(message, called_close);
    }
}
