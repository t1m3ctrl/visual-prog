#include "mainwindow.h"
#include "./ui_mainwindow.h"

#include "figure.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_listWidget_currentRowChanged(int currentRow)
{
    ui->label->hide();
    ui->label_2->hide();
    ui->label_3->hide();

    ui->lineEdit->hide();
    ui->lineEdit_2->hide();
    ui->lineEdit_3->hide();

    ui->lineEdit->clear();
    ui->lineEdit_2->clear();
    ui->lineEdit_3->clear();

    int currentFigure = currentRow;

    switch (currentFigure) {
    case square:
        figure_path = ":/images/figures/square_5854013.png";
        formula_path = ":/images/formulas/square.png";
        ui->label->setText("a:");
        ui->label->show();
        ui->lineEdit->show();
        break;
    case rectangle:
        figure_path = ":/images/figures/rectangle_5853969.png";
        formula_path = ":/images/formulas/rectangle.png";
        ui->label->setText("a:");
        ui->label_2->setText("b:");
        ui->label->show();
        ui->label_2->show();
        ui->lineEdit->show();
        ui->lineEdit_2->show();
        break;
    case parallelogram:
        figure_path = ":/images/figures/parallelogram_5853937.png";
        formula_path = ":/images/formulas/parallelogramm.png";
        ui->label->setText("a:");
        ui->label_2->setText("h:");
        ui->label->show();
        ui->label_2->show();
        ui->lineEdit->show();
        ui->lineEdit_2->show();
        break;
    case rhombus:
        figure_path = ":/images/figures/rhombus_5853863.png";
        formula_path = ":/images/formulas/rhombus.png";
        ui->label->setText("d_1:");
        ui->label_2->setText("d_2:");
        ui->label->show();
        ui->label_2->show();
        ui->lineEdit->show();
        ui->lineEdit_2->show();
        break;
    case triangle:
        figure_path = ":/images/figures/triangle_5854068.png";
        formula_path = ":/images/formulas/triangle.png";
        ui->label->setText("a:");
        ui->label_2->setText("h:");
        ui->label->show();
        ui->label_2->show();
        ui->lineEdit->show();
        ui->lineEdit_2->show();
        break;
    case trapezoid:
        figure_path = ":/images/figures/trapezoid_5854027.png";
        formula_path = ":/images/formulas/trapezoid.png";
        ui->label->setText("a:");
        ui->label_2->setText("b:");
        ui->label_3->setText("h:");
        ui->label->show();
        ui->label_2->show();
        ui->label_3->show();
        ui->lineEdit->show();
        ui->lineEdit_2->show();
        ui->lineEdit_3->show();
        break;
    case circle:
        figure_path = ":/images/figures/circle_5854033.png";
        formula_path = ":/images/formulas/circle.png";
        ui->label->setText("R:");
        ui->label->show();
        ui->lineEdit->show();
        break;
    case sector:
        figure_path = ":/images/figures/circle_5853989.png";
        formula_path = ":/images/formulas/sector.png";
        ui->label->setText("R:");
        ui->label_2->setText("α:");
        ui->label->show();
        ui->label_2->show();
        ui->lineEdit->show();
        ui->lineEdit_2->show();
        break;
    default:
        break;
    }
    QImage figure_image(figure_path);
    QImage formula_image(formula_path);
    ui->figureImage->setPixmap(QPixmap::fromImage(figure_image).scaled(200,200));
    ui->formula->setPixmap(QPixmap::fromImage(formula_image).scaled(200,200));
}

void MainWindow::on_pushButton_clicked()
{
    int currentFigure = ui->listWidget->currentRow();
    double p1 = ui->lineEdit->text().toDouble();
    double p2 = ui->lineEdit_2->text().toDouble();
    double p3 = ui->lineEdit_3->text().toDouble();
    ui->lineEdit_4->setText(QString::number(figure::calculateArea(currentFigure, p1, p2, p3)));
}


void MainWindow::on_action_triggered()
{
    QMessageBox::about(this, "Авторы", "ИП-211 Сотнич А.С.");
}

