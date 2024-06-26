﻿using BetterConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.MiniIssueTrackingSystem;

public class Program
{
    static List<Issue> issues = new List<Issue>();

    static void Main(string[] args)
    {
        // Example data
        issues.Add(new Issue(1, "Website Error", "Login page not working", "John Doe"));
        issues.Add(new Issue(2, "Database Slow Performance", "Database queries taking too long", "Jane Smith"));

        while (true)
        {
            Console.WriteLine("Issue Tracker v0.4");
            Console.WriteLine("1. List Issues");
            Console.WriteLine("2. Create Issue");
            Console.WriteLine("3. Update Issue");
            Console.WriteLine("4. Fix Issue");
            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListIssues();
                    break;
                case "2":
                    CreateIssue();
                    break;
                case "3":
                    UpdateIssue();
                    break;
                case "4":
                    FixIssue();
                    break;
                case "5":
                    Console.WriteLine("Exiting Issue Tracker.");
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void ListIssues()
    {
        if (issues.Count == 0)
        {
            Console.WriteLine("No issues found.");
            return;
        }

        Table table = new Table("Id", "Title", "Assigned To", "Created By", "Created Date", "Status", "Fixed Date");
        foreach (var issue in issues)
        {
            string fixedDate = issue.FixedDate.HasValue ? issue.FixedDate.Value.ToString("yyyy-MM-dd") : "Not Fixed";
            table.AddRow(
                issue.Id,
                issue.Title,
                issue.AssignedTo ?? "No Assignee",
                issue.CreatedBy,
                issue.CreatedDate.ToString("yyyy-MM-dd"),
                issue.Status,
                fixedDate);
        }

        Console.Write(table.ToString());
    }

    static void CreateIssue()
    {
        Console.Write("Enter your username: ");
        string createdBy = Console.ReadLine();

        Console.Write("Enter issue title: ");
        string title = Console.ReadLine();

        Console.Write("Enter issue description: ");
        string description = Console.ReadLine();

        int newId = issues.Count == 0 ? 1 : issues.Max(i => i.Id) + 1;
        issues.Add(new Issue(newId, title, description, createdBy));

        Console.WriteLine("Issue created successfully!");
    }

    static void UpdateIssue()
    {
        if (issues.Count == 0)
        {
            Console.WriteLine("No issues found to update.");
            return;
        }

        Console.WriteLine("List of Issues:");
        ListIssues();

        Console.Write("Enter the ID of the issue to update: ");
        int updateId;
        while (!int.TryParse(Console.ReadLine(), out updateId))
        {
            Console.WriteLine("Invalid ID. Please enter a valid integer.");
        }

        Issue issueToUpdate = issues.Find(i => i.Id == updateId);
        if (issueToUpdate == null)
        {
            Console.WriteLine($"Issue with ID {updateId} not found.");
            return;
        }

        Console.WriteLine("Current Assigned To: " + issueToUpdate.AssignedTo);
        Console.Write("Enter the new assigned user: ");
        string newAssignedTo = Console.ReadLine();

        issueToUpdate.AssignTo(newAssignedTo);
        Console.WriteLine($"Issue with ID {updateId} successfully updated. Assigned To: {newAssignedTo}");
    }

    static void FixIssue()
    {
        if (issues.Count == 0)
        {
            Console.WriteLine("No issues found to fix.");
            return;
        }

        Console.WriteLine("List of Issues:");
        ListIssues();

        Console.Write("Enter the ID of the issue to fix: ");
        int fixId;
        while (!int.TryParse(Console.ReadLine(), out fixId))
        {
            Console.WriteLine("Invalid ID. Please enter a valid integer.");
        }

        Issue issueToFix = issues.Find(i => i.Id == fixId);
        if (issueToFix == null)
        {
            Console.WriteLine($"Issue with ID {fixId} not found.");
            return;
        }

        issueToFix.FixIssue();
        Console.WriteLine($"Issue with ID {fixId} successfully fixed.");
    }
}